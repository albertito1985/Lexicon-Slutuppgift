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
            throw new ArgumentException("The input is empty.");
        }

    }

    public static bool StringLength(string stringInput, int maxCharacters, int minCharacters = 1)
    {
        if (stringInput.Length >= minCharacters && stringInput.Length <= maxCharacters)
        {
            return true;
        }
        return false;
    }
}
