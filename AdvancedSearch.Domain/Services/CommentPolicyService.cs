using AdvancedSearchDomain.Interfaces.Repositories;
using AdvancedSearchDomain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearchDomain.Services
{
    public class CommentPolicyService : ICommentPolicyService
    {
        private readonly IOrderRepository _orderRepository;

        public CommentPolicyService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public bool HasPurchased(int customerId, int productId)
        {
            return _orderRepository.HasPurchased(customerId, productId);
        }
    }
}
