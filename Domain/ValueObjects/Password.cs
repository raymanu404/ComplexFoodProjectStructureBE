using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record struct Password
{
    private readonly Regex hasMinimum8Chars = new(@".{8,}");
    private readonly Regex hasNumber = new(@"[0-9]+");
    private readonly Regex hasUpperChar = new(@"[A-Z]+");

    public Password(string value)
    {
        if (value == null) throw new ArgumentNullException("value");

        if (hasNumber.IsMatch(value) && hasUpperChar.IsMatch(value) && hasMinimum8Chars.IsMatch(value))
            Value = value;
        else
            throw new Exception("Password invalid!");
    }

    public string Value { get; set; }
}