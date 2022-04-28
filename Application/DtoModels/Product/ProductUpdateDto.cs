using Domain.ValueObjects;

namespace Application.DtoModels.Product
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }
        public Amount Amount { get; set; }
    }
}
