namespace Domain.ValueObjects;

public record struct Cantity
{
    public Cantity(int value)
    {
        if (value > 0)
            Value = value;
        else
            throw new Exception("Cantity invalid!");
    }

    public int Value { get; set; }
}