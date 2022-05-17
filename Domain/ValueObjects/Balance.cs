namespace Domain.ValueObjects;

public record struct Balance
{
    public double Value { get; set; }
    public Balance(double value)
    {
        if (value >= 0.0)
            Value = value;
        else
            throw new Exception("Balance invalid!");
    }

   
}