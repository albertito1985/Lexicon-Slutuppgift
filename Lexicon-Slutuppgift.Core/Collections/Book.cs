using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Core.Collections;

public class Book : Identifiable
{
    private string author;
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
        return $"Title: {Name}\n   Author: {Author}\n   ISBN: {IdNr}\n   Category: {Category}\n   {(OnLoan?"NOT available":"Available")}";
    }
}
