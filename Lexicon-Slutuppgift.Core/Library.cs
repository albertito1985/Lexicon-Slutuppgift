using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lexicon_Slutuppgift.Core;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift;

public static class Library
{
    public static Catalog catalog { get; set; } = new();
    public static void LoadLibrary()
    {
        if (File.Exists("library.json"))
        {   // Acá me quedé
            Catalog loadingCatalog = JsonSerializer.Deserialize<Catalog>(File.ReadAllText("library.json"));
            catalog = loadingCatalog;
        }
    }

    public static void ClearLibrary()
    {
        catalog.Clear();
        if (File.Exists("library.json"))
        {
            File.Delete("library.json");
        }
    }

    public static void AddBook(string author, string title)
    {
        Book newBook = new();
        newBook.Author = author.ToUpper();
        newBook.Title = title.ToUpper();
        newBook.GenerateISBN();

        catalog.Add(newBook);

        File.WriteAllText("library.json", JsonSerializer.Serialize(catalog));
    }

    public static Book SelectBook(string inputString)
    {
        var selection = catalog.Books.Where(B => B.Title == inputString.ToUpper());

        if (selection.Count() == 0)
        {
            selection = catalog.Books.Where(B => B.Isbn13 == inputString.ToUpper());
        }

        if (selection.Count() == 0)
        {
            throw new ArgumentException("Book not found");
        }

        return selection.First();            
    }

    public static void RemoveBook(Book inputBook)
    {

    }
}
