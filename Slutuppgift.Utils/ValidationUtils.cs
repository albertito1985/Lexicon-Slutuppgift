using System.Text.RegularExpressions;

namespace Slutuppgift.Utils;

public static class ValidationUtils
{
    public static bool String(string input)
    {
        if (!string.IsNullOrWhiteSpace(input))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static bool StringLength(string stringInput, int maxCharacters, int minCharacters = 1)
    {
        if (stringInput.Length >= minCharacters && stringInput.Length <= maxCharacters)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsNumber(string input)
    {
        if (Regex.IsMatch(input, @"^\d+$"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
