using AdvancedSearchDomain.Common;
using AdvancedSearchDomain.Interfaces.Services;
using ShopSage.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSage.Domain.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Information { get; private set; }
        public string Features { get; private set; }
        public string Description { get; private set; }

        private readonly List<ProductCategory> _productCategories = new();
        public IReadOnlyCollection<ProductCategory> ProductCategories => _productCategories.AsReadOnly();

        private readonly List<Comment> _comments = new();
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

        protected Product()
        {

        }

        public Product(string name, decimal price, int stock, string information, string features, string description)
        {
            Name = name;
            Price = price;
            Stock = stock;
            Information = information;
            Features = features;
            Description = description;
        }

        public void AddComment(string content, int customerId, ICommentPolicyService commentPolicyService)
        {
            if (commentPolicyService.HasPurchased(customerId, this.Id))
            {
                var comment = new Comment(content, customerId, this.Id);
                _comments.Add(comment);
            }
            else
            {
                throw new InvalidOperationException("Satın almadığınız bir ürüne yorum yapamazsınız.");
            }
        }
    }
}
