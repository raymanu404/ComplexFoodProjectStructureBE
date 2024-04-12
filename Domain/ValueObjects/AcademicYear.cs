namespace Domain.ValueObjects;

public record struct AcademicYear
{
    public int Value { get; set; }
    public AcademicYear(int value)
    {
        if (value is >= 1 and <= 6)
            Value = value;
        else
            throw new Exception("Academic year invalid!");
    }
}

   