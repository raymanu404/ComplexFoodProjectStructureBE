
namespace Domain.ValueObjects
{
    public record struct Price
    {
        public double Value { get; set; }
        public Price(double value)
        {            
            if(value > 0.0)
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
