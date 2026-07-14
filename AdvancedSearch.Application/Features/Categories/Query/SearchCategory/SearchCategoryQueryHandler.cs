using AdvancedSearch.Application.Interfaces.AI;
using AdvancedSearch.Application.Interfaces.Persistence;
using MediatR;

namespace AdvancedSearch.Application.Features.Categories.Query.SearchCategory
{
    public class SearchCategoryQueryHandler : IRequestHandler<SearchCategoryQuery, List<SearchCategoryQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmbeddingService _embeddingService;

        public SearchCategoryQueryHandler(IUnitOfWork unitOfWork, IEmbeddingService embeddingService)
        {
            _unitOfWork = unitOfWork;
            _embeddingService = embeddingService;
        }

        public async Task<List<SearchCategoryQueryResponse>> Handle(SearchCategoryQuery request, CancellationToken cancellationToken)
        {
            //1. Kullanıcının girdiği metni embedding'e çevir
            var querEmbedding =await _embeddingService.GenerateEmbeddingAsync(request.UserText);

            //2. Veritabanındaki kategorilerin embedding'leriyle karşılaştırarak benzer olanları bul
            var categories = await _unitOfWork.Categories.SearchByEmbeddingAsync(querEmbedding, request.TopK,cancellationToken);

            //3. Benzerlik skoruna göre filtrele
            var results=categories
                .Select(categories => new
                {
                    Category = categories,
                    Score = CosineSimilarity(querEmbedding, categories.Embedding)
                })
                .Where(x => x.Score >= (double)request.MinScore)
                .OrderByDescending(x=>x.Score)
                .Select(x=> new SearchCategoryQueryResponse(
                    Id: x.Category.Id,
                    Name: x.Category.CategoryName,
                    Score: Math.Round(x.Score, 4)
                )).ToList();

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
