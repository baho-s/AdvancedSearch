using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSage.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        public BaseEntity(Guid id)
        {
            Id=id;
        }
    }
}
