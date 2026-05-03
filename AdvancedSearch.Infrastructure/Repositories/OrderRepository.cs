using AdvancedSearch.Infrastructure.Context;
using AdvancedSearchDomain.Interfaces.Repositories;
using ShopSage.Domain.Entities;

namespace AdvancedSearch.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public bool HasPurchased(int customerId, int productId)
        {
             return _context.Orders.Where(o => o.CustomerId == customerId)
                .SelectMany(o => o.OrderItems)
                .Any(oi => oi.ProductId == productId);
        }
    }
}
