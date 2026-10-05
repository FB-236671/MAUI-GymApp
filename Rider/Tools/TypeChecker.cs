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
    
    public static string? IntegerCheck(string? input, string name, out int value)
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
        if (value <= 0)
        {
            return "Please input a number greater than zero for " + name;
        }
        return null;
    }
    
    public static string? IntegerCheck(string? input, string name, string index, out int value)
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
        if (value <= 0)
        {
            return "Please input a number greater than zero for " + name + " at " + index;
        }
        return null;
    }

    public static string? WordCheck(string? input, string name, string[] words)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "Please input something for " + name;
        }
        if (!words.Contains(input.Trim(), StringComparer.OrdinalIgnoreCase))
        {
            return "Please enter one of: " + string.Join(", ", words);
        }
        return null;
    }
}