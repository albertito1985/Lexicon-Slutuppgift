using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Core;

public class Book
{
    private string author;
    private string title;
    private string isbn13;
    private string category;
    public string Author
    { get => author;
        set
        {
            if (ValidationUtils.String(value))
            {
                author = value;
            }
            ;
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
            }
            ;
        }
    }
    public string Isbn13
    {
        get => isbn13;
        set
        {
            if (ValidationUtils.String(value) &&
                ValidationUtils.StringLength(value,13,13) &&
                ValidationUtils.IsNumber(value))
            {
                isbn13 = value;
            };
        }
    }
    public string Category
    {
        get => category;
        set
        {
            if (ValidationUtils.String(value))
            {
                category = value;
            };
        }
    }
    public bool OnLoan { get; set; } = false;

    public override string ToString()
    {
        return $"Title: {Title}\n   Author: {Author}\n   ISBN: {Isbn13}\n   Category: {Category}\n   {((OnLoan)?"NOT available":"Available")}";
    }
}
