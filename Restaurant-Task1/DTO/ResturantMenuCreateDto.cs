namespace Restaurant_Task1.DTO
{
    public class ResturantMenuCreateDto
    {
        public string MealName { get; set; }
        public double PriceInNis { get; set; }
        public double Quantity { get; set; }
        public int? RestaurantId { get; set; }

    }
}
