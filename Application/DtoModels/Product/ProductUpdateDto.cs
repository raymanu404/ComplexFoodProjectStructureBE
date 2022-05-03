using Domain.ValueObjects;

namespace Application.DtoModels.Product
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
