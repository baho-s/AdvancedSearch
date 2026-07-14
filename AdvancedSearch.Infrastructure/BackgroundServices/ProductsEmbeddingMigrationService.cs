using AdvancedSearch.Application.Interfaces.AI;
using AdvancedSearch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdvancedSearch.Infrastructure.BackgroundServices
{
    public class ProductsEmbeddingMigrationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ProductsEmbeddingMigrationService> _logger;//ILogger'da neden generic kullanıyoruz? Çünkü bu, log mesajlarının hangi sınıftan geldiğini belirtir ve logları daha okunabilir hale getirir.

        public ProductsEmbeddingMigrationService(IServiceScopeFactory scopeFactory,ILogger<ProductsEmbeddingMigrationService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();//neden using? Çünkü scope'un sonunda bellek sızıntısını önlemek için
                                                              //scope'u düzgün bir şekilde atmak istiyoruz.
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var embeddingService = scope.ServiceProvider.GetRequiredService<IEmbeddingService>();

                // Sadece embedding'i olmayan veya güncellenmesi gereken ürünleri çek
                var products = await context.Products
                    .Include(p => p.Comments) // Yorumları da dahil et
                    .Where(p => p.Embedding == null || p.IsEmbeddingDirty == true)
                    .ToListAsync(stoppingToken);

                if (!products.Any())
                {
                    _logger.LogInformation("Tüm ürünlerin embedding'i mevcut.");
                }

                _logger.LogInformation(
                "{Count} ürün için embedding üretiliyor...", products.Count);

                foreach (var product in products)
                {
                    try
                    {
                        var embedding = await embeddingService.GenerateEmbeddingAsync(product.BuildEmbeddingText());
                        product.UpdateEmbedding(embedding);
                        product.MarkEmbeddingAsClean();

                        // API rate limit'e takılmamak için küçük bekleme
                        await Task.Delay(200, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex,
                        "{ProductId} için embedding üretilemedi.", product.Id);
                    }
                }

                await context.SaveChangesAsync(stoppingToken);
                _logger.LogInformation("Products embedding migrasyonu tamamlandı.");
                _logger.LogInformation("Servis 30 saniye boyunca uykuya geçiyor...");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }   
        }
    }
}
