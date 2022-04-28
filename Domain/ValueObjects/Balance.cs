namespace Domain.ValueObjects;

public record struct Balance
{
    public float Value { get; set; }
    public Balance(float value)
    {
        if (value >= 0.0f)
            Value = value;
        else
            throw new Exception("Balance invalid!");
    }

   
}