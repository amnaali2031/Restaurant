using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant_Task1.Model
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            RestaurantMenus = new HashSet<RestaurantMenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Archived { get; set; }

        public virtual ICollection<RestaurantMenu> RestaurantMenus { get; set; }
    }
}
