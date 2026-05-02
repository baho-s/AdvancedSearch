using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSage.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int TotalPrice { get; private set; }
        public DateTime OrderDate { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }        

        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public Order(int totalPrice, DateTime orderDate, int customerId, Customer customer, OrderItem orderItem)
        {
            TotalPrice = totalPrice;
            OrderDate = orderDate;
            CustomerId = customerId;
            Customer = customer;
            _orderItems.Add(orderItem);
        }

    }
}
