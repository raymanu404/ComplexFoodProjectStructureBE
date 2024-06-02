using ApplicationAdmin.Contracts.Abstractions;
using Domain.Models.Enums;

namespace ApplicationAdmin.Features.Products.Queries.GetMostOrderedProducts;
public class Response
{
    public ResponseData<ProductDto> Data { get; set; }

    public class ProductDto
    {
        public int Id { get; set; }
        public Categories Category { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public string Image { get; set; } = null!;
        public DateTime DateUpdated { get; set; }
        public bool IsInStock { get; set; }
        public double MerchantPrice { get; set; }
        public int MostOrderedProductCount { get; set; }
    }
}
