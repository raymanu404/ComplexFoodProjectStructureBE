using System.Text.RegularExpressions;

namespace Domain.ValueObjects;
public record struct Gender
{
    private readonly Regex pattern = new(@"M|F|N");
    public Gender(string value)
    {
        if (value == null) throw new ArgumentNullException("value");

        if (pattern.IsMatch(value))
            Value = value;
        else
           Value = "";
    }

    public string Value { get; set; }
}