using Domain.Models.Enums;


namespace Application.DtoModels.Product
{
    public class ProductFromCartDto
    {
        public int Id { get; set; }
        public Categories Category { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string Image { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public int Cantity { get; set; }
    }
}
