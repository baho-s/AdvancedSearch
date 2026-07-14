using AdvancedSearchDomain.Interfaces.Repositories;
using AdvancedSearchDomain.Interfaces.Services;
using ShopSage.Domain.Entities;

namespace AdvancedSearchDomain.Services
{
    public class CommentPolicyService : ICommentPolicyService
    {
        private readonly IOrderRepository _orderRepository;
        public CommentPolicyService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task AddCommentToProductAsync(Guid customerId, Product product, string content)
        {
            var hasPurchased = await _orderRepository.HasPurchasedAsync(customerId, product.Id);
            if (!hasPurchased)
            {
                throw new InvalidOperationException("Yorum yapmak için ürünü satın almış olmalısınız.");
            }

            product.AddComment(content, customerId);
        }

        public async Task<bool> HasPurchasedAsync(Guid customerId, Guid productId)
        {
            return await _orderRepository.HasPurchasedAsync(customerId, productId);
        }
    }
}
