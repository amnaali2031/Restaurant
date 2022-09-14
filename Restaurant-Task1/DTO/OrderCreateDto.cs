namespace Restaurant_Task1.DTO
{
    public class OrderCreateDto
    {
        public int CustomerId { get; set; }
        public int RestaurantMenuId { get; set; }
        public double? Quantity { get; set; }

    }
}
