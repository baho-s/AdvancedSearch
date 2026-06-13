using AdvancedSearchDomain.Interfaces.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearch.Application.Features.ProductComment.Comamnd.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var product=await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if(product is null)
            {
                throw new Exception("Bu ürün bulunamadı");
            }
            Guid customerId= Guid.Parse("a3b85f64-5717-4562-b3fc-2c963f66afa6");
            product.AddComment(request.CommentText, customerId);
            await _unitOfWork.SaveChangesAsync();
            return product.Comments.Last().Id;
        }
    }
}
