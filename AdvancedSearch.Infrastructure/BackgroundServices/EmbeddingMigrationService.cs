using AdvancedSearch.Domain.Interfaces.Services;
using AdvancedSearch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdvancedSearch.Infrastructure.BackgroundServices
{
    public class EmbeddingMigrationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<EmbeddingMigrationService> _logger;//ILogger'da neden generic kullanıyoruz? Çünkü bu, log mesajlarının hangi sınıftan geldiğini belirtir ve logları daha okunabilir hale getirir.

        public EmbeddingMigrationService(IServiceScopeFactory scopeFactory,ILogger<EmbeddingMigrationService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope= _scopeFactory.CreateScope();//neden using? Çünkü scope'un sonunda bellek sızıntısını önlemek için
                                                         //scope'u düzgün bir şekilde atmak istiyoruz.
            var context=scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var embeddingService=scope.ServiceProvider.GetRequiredService<IEmbeddingService>();

            // Sadece embedding'i olmayan ürünleri çek
            var products = await context.Products
                .Where(p => p.Embedding == null)
                .ToListAsync(stoppingToken);

            if (!products.Any())
            {
                _logger.LogInformation("Tüm ürünlerin embedding'i mevcut.");
                return;
            }

            _logger.LogInformation(
            "{Count} ürün için embedding üretiliyor...", products.Count);

            foreach(var product in products)
            {
                try
                {
                    var embedding = await embeddingService.GenerateEmbeddingAsync(product.GetEmbeddingText());
                    product.UpdateEmbedding(embedding);

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
            _logger.LogInformation("Embedding migrasyonu tamamlandı.");

        }
    }
}
