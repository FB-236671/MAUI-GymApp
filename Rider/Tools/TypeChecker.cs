using System.Globalization;

namespace MAUI_GymApp;

public static class TypeChecker
{
    public static string? StringCheck(string? input, string name)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "Please input something for " + name;
        }
        return null;
    }

    public static string? IntegerCheck(string? input, string name, int min, int max, out int value)
    {
        value = 0;
        if (string.IsNullOrWhiteSpace(input))
        {
            return "Please input something for " + name;
        }
        if (!int.TryParse(input, out value))
        {
            return "Please input a whole number for " + name;
        }
        if (value > max || value < min)
        {
            return "Please input something between " + min + " and " + max;
        }
        return null;
    }
    
    public static string? IntegerIndexCheck(string? input, string name, string index, int min, int max, out int value)
    {
        value = 0;
        if (string.IsNullOrWhiteSpace(input))
        {
            return "Please input something for " + name + " at " + index;
        }
        if (!int.TryParse(input, out value))
        {
            return "Please input a whole number for " + name + " at " + index;
        }
        if (value > max || value < min)
        {
            return "Please input something between " + min + " and " + max + " at " + index;
        }
        return null;
    }

    public static string? DecimalIndexCheck(string? input, string name, string index, decimal min, decimal max, out decimal value)
    {
        value = 0;
        if (string.IsNullOrWhiteSpace(input))
        {
            return "Please input something for " + name + " at " + index;
        }
        if (!decimal.TryParse(input.Trim(), NumberStyles.AllowDecimalPoint, null, out value))
        {
            return "Please input a number for " + name + " at " + index;
        }
        if (value > max || value < min)
        {
            return "Please input something between " + min + " and " + max + " at " + index;
        }
        if (decimal.Round(value, 1) != value)
        {
            return "Please use no more than 1 decimal place for " + name + " at " + index;
        }
        return null;
    }
}