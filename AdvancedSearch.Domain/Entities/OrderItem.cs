using ShopSage.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSage.Domain.Entities
{
    public class OrderItem:BaseEntity
    {
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Guid OrderId { get; private set; }
        public Order Order { get; private set; }
        public Guid ProductId { get; private set; }
        public virtual Product Product { get; private set; } = null!;

        protected OrderItem()
        {
            
        }

        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = quantity * unitPrice;
        }

        //APİDEN DTO İLE ProductId ve Quantity alacağım ve CustomerId token'dan gelecek.
        //Şimdi bir siparişte birden fazla ürün olabilir farklı farklı Bunu nasıl yapabilirim.
        //Order'da OrderItem'ı list olarak tuttum ve dışarıya erişimini kapattım şimdi her ürün
        //seçildiğinde bunu Order entity'sindeki Listeye eklemem gerekiyor ekledikten sonra 
        //Toplam sipariş tutarınıda ayarlamam gerek şimdi nasıl yaparım bunu düşünelim.
    }
}
