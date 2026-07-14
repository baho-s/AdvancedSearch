using AdvancedSearch.Domain.Interfaces.Repositories;
using AdvancedSearch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using ShopSage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Infrastructure.Repositories
{
    public class ProductRepository:GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context) 
        {
        }

        public async Task<List<Product>> SearchByEmbeddingAsync(float[] queryEmbedding, int topK, CancellationToken cancellationToken)
        {
            var vector=new Vector(queryEmbedding);

            return await _context.Products
                .AsNoTracking()
                .Where(p=>p.Embedding!=null && p.Stock>0 && !p.IsDeleted)//Embedding'i olan ve stokta bulunan ürünleri filtrele
                .OrderBy(p=> p.Embedding!.CosineDistance(vector))//Embedding'ler arasındaki kosinüs benzerliğine göre sırala
                .Take(topK)//En benzer topK ürünü al
                .ToListAsync(cancellationToken);
        }
    }
}
