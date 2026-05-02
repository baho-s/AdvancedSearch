using ShopSage.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSage.Domain.Entities
{
    public class Comment:BaseEntity
    {
        public string Content { get; private set; }

        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }

        protected Comment()
        {

        }

        public Comment(string content, int customerId, int productId)
        {
            Content = content;
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}
