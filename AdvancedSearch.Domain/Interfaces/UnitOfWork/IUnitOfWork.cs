using AdvancedSearch.Domain.Interfaces.Repositories;
using AdvancedSearchDomain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearchDomain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        ICategoryRepository Categories { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken=default);
    }
}

