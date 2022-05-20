using Domain.Models.Enums;

namespace Application.DtoModels.OrderItem
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Cantity { get; set; }
        public Categories Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }   
        public int OrderId { get; set; }
    }
}
