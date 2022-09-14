using Restaurant_Task1.Extensions;

namespace Restaurant_Task1.ModelView
{
    public class RestaurantViewModel
    {
        public RestaurantViewModel()
        {
            
        }
        public int? NumberOfOrderedCustomer { get; set; }
        public string RestaurantName { get; set; }
        public double? ProfitInNis { get; set; }
        public double? ProfitInUsd { get; set; }
        public string TheBestSellingMeal { get; set; }
        public string MostPurchasedCustomer { get; set; }
    }
}
