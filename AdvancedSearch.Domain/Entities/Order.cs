using AdvancedSearchDomain.Common;
using AdvancedSearchDomain.ValueObjects;
using ShopSage.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSage.Domain.Entities
{
    public class Order: BaseEntity,IAggregateRoot
    {
        public decimal TotalPrice { get; private set; }
        public DateTime OrderDate { get; private set; } 
        public int CustomerId { get; private set; }
        public Address Address { get; private set; }
        public Customer Customer { get; private set; }

        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        protected Order()
        {

        }

        public void AddOrderItem(OrderItem orderItem)
        {
            _orderItems.Add(orderItem);
            TotalPrice += orderItem.TotalPrice;
        }   

        public Order(Customer customer, Address address)
        {
            OrderDate = DateTime.UtcNow;
            Customer=customer;
            CustomerId=customer.Id;
            Address = address;
        }

        public void UpdateAddress(Address address)
        {
            Address = address;
        }
    }
}
