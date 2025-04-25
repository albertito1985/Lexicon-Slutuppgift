using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Core;

public class Book
{
    private string author;
    private string title;
    private string isbn13;
    private string cathegory;
    public string Author
    { get => author;
        set
        {
            if (ValidationUtils.String(value))
            {
                author = value;
            };
        }
    }
    public string Title
    {
        get => title;
        set
        {
            if (ValidationUtils.String(value))
            {
                title = value;
            };
        }
    }
    public string Isbn13
    {
        get => isbn13;
        set
        {
            if (ValidationUtils.String(value) &&
                ValidationUtils.StringLength(value,13,13))
            {
                isbn13 = value;
            };
        }
    }
    public string Cathegory
    {
        get => cathegory;
        set
        {
            if (ValidationUtils.String(value))
            {
                cathegory = value;
            };
        }
    }
    public bool OnLoan { get; set; } = false;

    public void GenerateISBN()
    {
        Random random = new Random();
        string result = "";

        for (int i = 0; i < 13; i++)
        {
            result += random.Next(0, 10);
        }

        Isbn13 = result;
    }
}
