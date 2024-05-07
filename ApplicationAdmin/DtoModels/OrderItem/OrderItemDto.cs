using Domain.Models.Enums;

namespace ApplicationAdmin.DtoModels.OrderItem
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int Cantity { get; set; }
        public Categories Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }   
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
