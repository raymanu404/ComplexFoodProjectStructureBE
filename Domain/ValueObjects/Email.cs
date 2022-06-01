using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record struct Email
{
    private const string Pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    private static readonly Regex validCode = new(Pattern);
    public Email(string value)
    {
        if (validCode.IsMatch(value))
            Value = value;
        else
            Value = "";
    }

    public string Value { get; set; }
}