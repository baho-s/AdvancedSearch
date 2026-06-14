using AdvancedSearchDomain.Interfaces.Repositories;
using AdvancedSearchDomain.Interfaces.Services;
using AdvancedSearchDomain.Interfaces.UnitOfWork;
using ShopSage.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedSearchDomain.Services
{
    public class CommentPolicyService : ICommentPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentPolicyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddCommentToProductAsync(Guid customerId, Product product, string content)
        {
            var hasPurchased = await _unitOfWork.Orders.HasPurchasedAsync(customerId, product.Id);
            if (!hasPurchased)
            {
                throw new InvalidOperationException("Yorum yapmak için ürünü satın almış olmalısınız.");
            }

            product.AddComment(content, customerId);
        }

        public async Task<bool> HasPurchasedAsync(Guid customerId, Guid productId)
        {
            return await _unitOfWork.Orders.HasPurchasedAsync(customerId, productId);
        }
    }
}
