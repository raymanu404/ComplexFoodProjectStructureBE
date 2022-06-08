using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record struct PhoneNumber
{
    private readonly Regex pattern = new(@"^(\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\s|\.|\-)?([0-9]{3}(\s|\.|\-|)){2}$");
    private readonly Regex pattern2 = new(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

    public PhoneNumber(string value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        Value = pattern2.IsMatch(value) ? value : "";
    }

    public string Value { get; set; }
}