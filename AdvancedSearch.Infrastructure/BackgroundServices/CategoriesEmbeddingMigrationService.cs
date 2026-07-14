using AdvancedSearch.Application.Interfaces.AI;
using AdvancedSearch.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AdvancedSearch.Infrastructure.BackgroundServices
{
    public class CategoriesEmbeddingMigrationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CategoriesEmbeddingMigrationService> _logger;
        public CategoriesEmbeddingMigrationService(IServiceScopeFactory scopeFactory, ILogger<CategoriesEmbeddingMigrationService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope= _scopeFactory.CreateScope();

            var context=scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var embeddingService=scope.ServiceProvider.GetRequiredService<IEmbeddingService>();

            var categories = context.Categories.Where(c => c.Embedding == null).ToList();

            if(!categories.Any())
            {
                _logger.LogInformation("Tüm kategorilerin embedding'i mevcut.");
                return;
            }

            _logger.LogInformation(
                "{Count} kategori için embedding üretiliyor...", categories.Count);

            foreach(var category in categories)
            {
                try
                {
                    var embedding = await embeddingService.GenerateEmbeddingAsync(category.CategoryName);
                    category.UpdateEmmbedding(embedding);

                    await Task.Delay(200, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, 
                        "Kategori embedding üretimi sırasında bir hata oluştu: {CategoryId}", category.Id);
                }
            }
            await context.SaveChangesAsync(stoppingToken);
            _logger.LogInformation("Kategori embedding migrasyonu tamamlandı.");
        }
    }
}
