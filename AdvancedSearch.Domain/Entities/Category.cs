using AdvancedSearchDomain.Common;
using ShopSage.Domain.Common;

namespace ShopSage.Domain.Entities
{
    public class Category:BaseEntity, IAggregateRoot
    {
        public string CategoryName { get; set; }

        public float[]? Embedding { get; private set; }

        private readonly List<ProductCategory> _productCategories = new();
        public IReadOnlyCollection<ProductCategory> ProductCategories => _productCategories.AsReadOnly();

        protected Category()
        {
        }

        public Category(Guid id, string categoryName)
        {
            Id=id;
            CategoryName = categoryName;
        }
        
        public void UpdateEmmbedding(float[]? embedding)
        {
            Embedding = embedding;
        }
    }
}
