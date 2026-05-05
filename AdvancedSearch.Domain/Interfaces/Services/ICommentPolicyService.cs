using ShopSage.Domain.Entities;

namespace AdvancedSearchDomain.Interfaces.Services
{
    public interface ICommentPolicyService
    {
        Task<bool> HasPurchasedAsync(Guid customerId, Guid productId);

        Task AddCommentToProductAsync(Guid customerId, Product product, string content);
    }
}
