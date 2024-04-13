

using System;
using System.Reflection;

namespace HelperLibrary.Methods;
public static class Helpers
{
    public static string ToUpperFirstChar(this string input)
    {
        // Check if the input string is null or empty
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        // Capitalize the first character and concatenate with the rest of the string
        return char.ToUpper(input[0]) + input.Substring(1);
    }

    public static Type MapPropertyType<T>(string columnName) where T : class
    {
        PropertyInfo property = typeof(T).GetProperty(columnName.ToUpperFirstChar(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (property == null)
        {
            throw new ArgumentException($"Property '{columnName}' not found in type '{typeof(T).FullName}'.");
        }

        return property.PropertyType;
    }

    public static T ConvertToEnum<T>(this int value, T backup) where T : struct, Enum
    {
        if (Enum.TryParse<T>(value.ToString(), out T enumValue) && Enum.IsDefined(typeof(T), enumValue))
        {
            return enumValue;
        }

        return backup;
    }

    public static bool IsNotSmallerThan(this int status, int thresholdStatus) 
    {
        
        return status >= thresholdStatus;
    }
}
