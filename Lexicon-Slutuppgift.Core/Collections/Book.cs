using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Core.Collections;

public class Book : IIdentifiable
{
    private string author;
    private string name;
    private string idNr;
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
    public string Name
    {
        get => name;
        set
        {
            if (ValidationUtils.String(value))
            {
                name = value;
            }
            ;
        }
    }
    public string IdNr
    {
        get => idNr;
        set
        {
            if (ValidationUtils.String(value) &&
                ValidationUtils.StringLength(value,13,13) &&
                ValidationUtils.IsNumber(value))
            {
                idNr = value;
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
        return $"Title: {Name}\n   Author: {Author}\n   ISBN: {IdNr}\n   Category: {Category}\n   {(OnLoan?"NOT available":"Available")}";
    }
}
