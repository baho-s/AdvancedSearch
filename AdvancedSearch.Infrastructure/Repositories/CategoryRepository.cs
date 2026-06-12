using AdvancedSearch.Domain.Interfaces.Repositories;
using AdvancedSearch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using ShopSage.Domain.Entities;

namespace AdvancedSearch.Infrastructure.Repositories
{
    public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context): base(context) { }

        public async Task<List<Category>> SearchByEmbeddingAsync(float[] queryEmbedding, int topK, CancellationToken cancellationToken)
        {
            var vector = new Vector(queryEmbedding);

            return await _context.Categories
                .Where(p => p.Embedding != null)
                .OrderBy(p => p.Embedding!.CosineDistance(vector))
                .Take(topK)
                .ToListAsync(cancellationToken);
        }
    }
}
