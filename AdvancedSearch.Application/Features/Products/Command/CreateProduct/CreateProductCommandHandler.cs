using AdvancedSearch.Domain.Interfaces.Services;
using AdvancedSearchDomain.Interfaces.UnitOfWork;
using MediatR;
using ShopSage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.Products.Command.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmbeddingService _embeddingService;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IEmbeddingService embeddingService)
        {
            _unitOfWork = unitOfWork;
            _embeddingService = embeddingService;
        }

        public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var category=await _unitOfWork.Categories.GetByIdAsync(command.CategoryId,cancellationToken);
            if(category is null)
            {
                throw new Exception("Kategori bulunamadı");
            }
            var product=new Product(Guid.NewGuid(), category.Id,command.Name,command.Price,
                command.Stock,command.Information,command.Features,command.Description);

            var embeddingText = product.BuildEmbeddingText();
            var embedding = await _embeddingService.GenerateEmbeddingAsync(embeddingText);
            product.UpdateEmbedding(embedding);

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return product.Id;
        }
    }
}
