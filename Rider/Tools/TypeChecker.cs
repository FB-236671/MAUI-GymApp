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
        if (value <= 0)
        {
            return "Please input a number greater than zero for " + name;
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
        if (value <= 0)
        {
            return "Please input a number greater than zero for " + name + " at " + index;
        }
        return null;
    }

    public static string? DoubleCheck(string? input, string name, out double value)
    {
        value = 0;
        if (string.IsNullOrWhiteSpace(input))
        {
            return "Please input something for " + name;
        }
        if (!double.TryParse(input, out value))
        {
            return "Please input a number for " + name;
        }
        return null;
    }
}