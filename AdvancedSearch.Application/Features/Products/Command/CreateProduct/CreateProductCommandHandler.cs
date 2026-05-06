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

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return product.Id;
        }
    }
}
