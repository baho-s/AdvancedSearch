using AdvancedSearch.Domain.Interfaces.Repositories;
using AdvancedSearchDomain.Interfaces.Repositories;

namespace AdvancedSearch.Application.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        ICategoryRepository Categories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
