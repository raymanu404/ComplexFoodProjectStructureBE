using Domain.Models.Enums;

namespace ApplicationAdmin.DtoModels.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public Categories Category { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string Image { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsInStock { get; set; }
        public double MerchantPrice { get; set; }
    }
}
