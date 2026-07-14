using AdvancedSearch.Application.Interfaces.AI;
using AdvancedSearchDomain.Interfaces.UnitOfWork;
using MediatR;

namespace AdvancedSearch.Application.Features.Products.Query.SearchProducts
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, List<SearchProductsQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmbeddingService _embeddingService;

        public SearchProductsQueryHandler(IUnitOfWork unitOfWork, IEmbeddingService embeddingService)
        {
            _unitOfWork = unitOfWork;
            _embeddingService = embeddingService;
        }

        public async Task<List<SearchProductsQueryResponse>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            // 1. Kullanıcının yazdığı metni vektöre çevir
            // "Kırmızı düğün ayakkabısı" → [0.23, -0.11, 0.87, ...]
            var queryEmbedding = await _embeddingService.GenerateEmbeddingAsync(request.UserText);

            // 2. Repository üzerinden benzer ürünleri getir
            var products = await _unitOfWork.Products.SearchByEmbeddingAsync(queryEmbedding, request.TopK,cancellationToken);

            // 3. Score hesapla ve MinScore filtresi uygula
            // Cosine similarity: vektörler ne kadar benzer → 1'e ne kadar yakın
            var results = products
                .Select(p => new
                {
                    Product = p,
                    // Dot product ile cosine similarity hesapla
                    Score = CosineSimilarity(queryEmbedding, p.Embedding!)
                })
                .Where(x => x.Score >= (double)request.MinScore)
                .OrderByDescending(x => x.Score)
                .Select(x => new SearchProductsQueryResponse(
                    x.Product.Id,
                    x.Product.Name,
                    x.Product.Price,
                    x.Product.Stock,
                    x.Product.Description,
                    x.Product.Features,
                    Math.Round(x.Score, 4) // 0.8923 gibi
                ))
                .ToList();

            return results;
        }

        // Cosine similarity: iki vektörün ne kadar benzer olduğunu hesaplar
        // Sonuç 0-1 arası: 1 = aynı, 0 = tamamen farklı
        private static double CosineSimilarity(float[] a, float[] b)
        {
            // Vektör boyutları eşit değilse bir şeyler yanlış gitmiş demektir
            if (a.Length != b.Length)
                throw new InvalidOperationException(
                    $"Vektör boyutları eşleşmiyor: {a.Length} != {b.Length}");

            double dot = 0, normA = 0, normB = 0;

            for (int i = 0; i < a.Length; i++)
            {
                dot += a[i] * b[i];
                normA += a[i] * a[i];
                normB += b[i] * b[i];
            }

            // Sıfıra bölünme koruması
            if (normA == 0 || normB == 0) return 0;

            return dot / (Math.Sqrt(normA) * Math.Sqrt(normB));
        }
    }
}
