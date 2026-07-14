using AdvancedSearch.Application.Interfaces.Persistence;
using AdvancedSearch.Domain.Interfaces.Repositories;
using AdvancedSearch.Infrastructure.Context;
using AdvancedSearch.Infrastructure.Repositories;
using AdvancedSearchDomain.Interfaces.Repositories;

namespace AdvancedSearch.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IOrderRepository Orders { get; private set; }
        public IProductRepository Products { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public UnitOfWork(AppDbContext context,IOrderRepository orderRepository,IProductRepository productRepository,ICustomerRepository customerRepository,ICategoryRepository categoryRepository)
        {
            _context = context;
            Orders = orderRepository;
            Products = productRepository;
            Customers = customerRepository;
            Categories = categoryRepository;
        }



        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken=default)
        {
            return await _context.SaveChangesAsync(cancellationToken);   
        }
    }
}
