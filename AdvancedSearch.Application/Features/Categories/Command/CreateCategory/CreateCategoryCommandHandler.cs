using AdvancedSearch.Application.Interfaces.AI;
using AdvancedSearch.Application.Interfaces.Persistence;
using MediatR;
using ShopSage.Domain.Entities;

namespace AdvancedSearch.Application.Features.Categories.Command.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmbeddingService _embeddingService;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IEmbeddingService embeddingService)
        {
            _unitOfWork = unitOfWork;
            _embeddingService = embeddingService;
        }

        public async Task<Guid> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category=new Category(Guid.NewGuid(),command.CategoryName);
            
            var embeddingText=category.GetEmbeddingText();
            var embedding=await _embeddingService.GenerateEmbeddingAsync(embeddingText);

            category.UpdateEmmbedding(embedding);

            await _unitOfWork.Categories.AddAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            return category.Id;
        }
    }
}
