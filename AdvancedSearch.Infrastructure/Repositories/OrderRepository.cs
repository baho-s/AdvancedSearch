using AdvancedSearch.Infrastructure.Context;
using AdvancedSearchDomain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using ShopSage.Domain.Entities;

namespace AdvancedSearch.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> HasPurchasedAsync(Guid customerId, Guid productId,CancellationToken cancellationToken=default)
        {
             return await _context.Orders.Where(o => o.CustomerId == customerId)
                .SelectMany(o => o.OrderItems)
                .AnyAsync(oi => oi.ProductId == productId,cancellationToken);
        }
    }
}
