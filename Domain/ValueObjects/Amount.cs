namespace Domain.ValueObjects;

public record struct Amount
{
    public Amount(int value)
    {
        if (value > 0)
            Value = value;
        else
            throw new Exception("Amount invalid!");
    }

    public int Value { get; set; }
}