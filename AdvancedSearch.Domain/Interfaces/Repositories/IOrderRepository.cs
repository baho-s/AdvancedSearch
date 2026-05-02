using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearchDomain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        bool HasPurchased(int customerId,int productId);
    }
}
