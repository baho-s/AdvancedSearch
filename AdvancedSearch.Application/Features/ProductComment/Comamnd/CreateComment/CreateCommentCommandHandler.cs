using AdvancedSearch.Application.Interfaces.Persistence;
using AdvancedSearchDomain.Interfaces.Services;
using MediatR;

namespace AdvancedSearch.Application.Features.ProductComment.Comamnd.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommentPolicyService _commentPolicyService;
        public CreateCommentCommandHandler(IUnitOfWork unitOfWork, ICommentPolicyService commentPolicyService)
        {
            _unitOfWork = unitOfWork;
            _commentPolicyService = commentPolicyService;
        }

        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var product=await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if(product is null)
            {
                throw new Exception("Bu ürün bulunamadı");
            }
            Guid customerId= Guid.Parse("a3b85f64-5717-4562-b3fc-2c963f66afa6");
            await _commentPolicyService.AddCommentToProductAsync(customerId,product, request.CommentText);
            await _unitOfWork.SaveChangesAsync();
            return product.Comments.Last().Id;
        }
    }
}
