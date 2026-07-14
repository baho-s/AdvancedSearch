using AdvancedSearch.Application.Interfaces.AI;
using AdvancedSearch.Application.Interfaces.Persistence;
using MediatR;

namespace AdvancedSearch.Application.Features.Products.Query.AskProductQuestion
{
    public class AskProductQueryHandler : IRequestHandler<AskProductQuery, AskProductQueryResponse>
    {
        private readonly IRagService _ragService;
        private readonly IEmbeddingService _embeddingService;
        private readonly IUnitOfWork _unitOfWork;

        public AskProductQueryHandler(IRagService ragService, IEmbeddingService embeddingService, IUnitOfWork unitOfWork)
        {
            _ragService = ragService;
            _embeddingService = embeddingService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AskProductQueryResponse> Handle(AskProductQuery request, CancellationToken cancellationToken)
        {
            var queryEmbedding = await _embeddingService.GenerateEmbeddingAsync(request.Question);

            var products=await _unitOfWork.Products.SearchByEmbeddingAsync(queryEmbedding, 5,cancellationToken);

            var response=await _ragService.GenerateAnswerAsync(request.Question, products, cancellationToken);

            return new AskProductQueryResponse { Response = response };
        }
    }
}
