
namespace Domain.ValueObjects
{
    public record struct Price
    {
        public float Value { get; set; }
        public Price(float value)
        {            
            if(value > 0)
            {
                Value = value;
            }
            else
            {
                throw new Exception("Price invalid!");
            }
        }
    }
}
