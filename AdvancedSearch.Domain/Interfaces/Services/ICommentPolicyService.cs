using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearchDomain.Interfaces.Services
{
    public interface ICommentPolicyService
    {
        bool HasPurchased(Guid customerId, Guid productId);
    }
}
