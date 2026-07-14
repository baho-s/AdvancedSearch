using ShopSage.Domain.Entities;

namespace AdvancedSearchDomain.Interfaces.Repositories
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<bool> HasPurchasedAsync(Guid customerId, Guid productId,CancellationToken cancellationToken=default);
    }
}
