using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record struct MatriculationNumber
{
    //MO23232
    private readonly Regex onlyLetters = new(@"^(MO|LO|DO)\d{4}$");

    public MatriculationNumber(string value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        Value = onlyLetters.IsMatch(value) ? value : "";
    }

    public string Value { get; set; }
}