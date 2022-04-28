
namespace Domain.ValueObjects
{
    public record struct Discount
    {
        public int Value { get; set; }
        public Discount(int value)
        {
            Value = value >= 0 ? value : throw new Exception("Discount invalid!"); ;
        }
    }
}
