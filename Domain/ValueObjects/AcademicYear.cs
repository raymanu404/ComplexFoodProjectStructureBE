namespace Domain.ValueObjects;

public record struct AcademicYear
{
    public int Value { get; set; }
    public AcademicYear(int value)
    {
        if (value >= 1 && value <= 6)
            Value = value;
        else
            throw new Exception("Academic year invalid!");
    }
}

   