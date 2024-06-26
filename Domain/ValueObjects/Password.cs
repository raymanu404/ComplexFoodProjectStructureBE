﻿using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public record struct Password
{
    private readonly Regex hasMinimum8Chars = new(@"^.{8,40}$");
    //private readonly Regex hasMinimum8Chars = new(@"\s*\S.{8,}\S\s*");
    private readonly Regex hasNumber = new(@"[0-9]+");
    private readonly Regex hasUpperChar = new(@"[A-Z]+");

    public Password(string value)
    {
        if (value == null) throw new ArgumentNullException("value");

        if (hasNumber.IsMatch(value))
            Value = value;
        else
            Value = "";
        
    }
    public Password(string value, bool hashPass = false)
    {
        if (hashPass)
        {
            Value = value;
        }
        else
        {
            throw new Exception("Password Invalid!");
        }
    }

    public string Value { get; set; }

}