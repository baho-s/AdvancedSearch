using AdvancedSearchDomain.Common;
using ShopSage.Domain.Common;
using System.Numerics;

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

        public float[]? Embedding { get; private set; }

        private readonly List<ProductCategory> _productCategories = new();
        public IReadOnlyCollection<ProductCategory> ProductCategories => _productCategories.AsReadOnly();

        private readonly List<Comment> _comments = new();
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

        protected Product()
        {

        }

        public Product(Guid productId,Guid categoryId, string name, decimal price, int stock, string information, string features, string description)
        {
            Id = productId;
            _productCategories.Add(new ProductCategory
            {
                Product=this,
                ProductId = productId,
                CategoryId = categoryId
            });
            Name = name;
            Price = price;
            Stock = stock;
            Information = information;
            Features = features;
            Description = description;
        }

        internal void AddComment(string content, Guid customerId)//neden internal? Çünkü sadece Product sınıfı içinde kullanılacak ve dışarıdan erişim sağlanmayacak.
                                                                 //Bu yöntem, ürünle ilgili yorum eklemek için kullanılacak ve sadece ürünün kendisi tarafından çağrılabilir.
        {
            var comment = new Comment(content, customerId, this.Id);
            _comments.Add(comment);            
        }

        public void SetEmbedding(float[]? embedding)
        {
            Embedding = embedding;
        }
        
        public void UpdateEmbedding(float[]? embedding)
        {
            Embedding = embedding;
        }

        // Embedding için zengin metin üretir
        // Infrastructure veya Application bu metodu çağırır
        public string GetEmbeddingText()=> $"{Name} {Information} {Features} {Description}";
    }
}
