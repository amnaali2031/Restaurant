using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant_Task1.Model
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Archived { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
