using AdvancedSearchDomain.Interfaces.Services;
using ShopSage.Domain.Entities;

namespace AdvancedSearchDomain.Services
{
    public class CommentPolicyService : ICommentPolicyService
    {
        //private readonly IUnitOfWork _unitOfWork;

        public CommentPolicyService()
        {
            //_unitOfWork = unitOfWork;
        }

        public async Task AddCommentToProductAsync(Guid customerId, Product product, string content)
        {
            /*var hasPurchased = await _unitOfWork.Orders.HasPurchasedAsync(customerId, product.Id);
            if (!hasPurchased)
            {
                throw new InvalidOperationException("Yorum yapmak için ürünü satın almış olmalısınız.");
            }

            product.AddComment(content, customerId);*/
        }

        public async Task<bool> HasPurchasedAsync(Guid customerId, Guid productId)
        {
            //return await _unitOfWork.Orders.HasPurchasedAsync(customerId, productId);
            return true; // Geçici olarak her zaman true döndürüyor
        }
    }
}
