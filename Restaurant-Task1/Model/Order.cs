using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant_Task1.Model
{
    public partial class Order
    {
        public int CustomerId { get; set; }
        public int RestaurantMenuId { get; set; }
        public double? Quantity { get; set; }
        public int Id { get; set; }
        public double? TotalPrice { get; set; }
        public int Archived { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual RestaurantMenu RestaurantMenu { get; set; }
    }
}
