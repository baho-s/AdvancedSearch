using ShopSage.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSage.Domain.Entities
{
    public class Customer: BaseEntity
    {
        public int CustomerId { get; set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public string TelNo { get; private set; } = string.Empty;   

        private readonly List<Comment> _comments = new();
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly(); 
        

        public Customer(string name, string surname, string address, string email, string telNo)
        {
            Name = name;
            Surname = surname;
            Address = address;
            Email = email;
            TelNo = telNo;
        }

        public void EmailUpdate(string email)
        {
            Email = email;
        }

        public void AddressUpdate(string address)
        {
            Address = address;
        }
    }
}
