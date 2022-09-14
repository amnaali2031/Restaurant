using System;

namespace Restaurant_Task1.ModelView
{
    public class OrderModelView
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int RestaurantMenuId { get; set; }
        public double? Quantity { get; set; }
        public double? TotalPrice { get; set; }
        public int Archived { get; set; }


    }
}
