using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record struct UniqueCode
{
    private readonly Regex validCode = new(@"^[a-zA-Z0-9]+$");

    public UniqueCode(string value)
    {
        if (value == null) throw new ArgumentNullException("value");

        if (validCode.IsMatch(value) && value.Length == 6)
            Value = value;
        else
            throw new Exception("UniqueCode invalid!");
    }

    public string Value { get; set; }
}