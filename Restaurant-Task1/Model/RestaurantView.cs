using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant_Task1.Model
{
    public partial class RestaurantView
    {
        public int? NumberOfOrderedCustomer { get; set; }
        public string Name { get; set; }
        public double? ProfitInNis { get; set; }
        public double? ProfitInUsd { get; set; }
        public string TheBestSellingMeal { get; set; }
        public string Expr1 { get; set; }
    }
}
