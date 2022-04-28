
namespace Domain.ValueObjects
{
    public record struct Price
    {
        public float Value { get; set; }
        public Price(float value)
        {            
            Value = value > 0 ? value : throw new Exception("Price invalid!"); ;
        }
    }
}
