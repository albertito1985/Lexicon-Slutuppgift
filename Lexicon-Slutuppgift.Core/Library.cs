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
    public static List<Book> Catalog { get; set; } = new();
    public static void LoadLibrary()
    {
        if (File.Exists("library.json"))
        {   // Acá me quedé
            List<Book> loadingCatalog = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText("library.json"));
            Catalog = loadingCatalog;
        }
    }

    public static void ClearLibrary()
    {
        Catalog.Clear();
        if (File.Exists("library.json"))
        {
            File.Delete("library.json");
        }
    }

    public static void AddBook(string author, string title, string category)
    {
        Book newBook = new();
        newBook.Author = author.ToUpper();
        newBook.Title = title.ToUpper();
        newBook.Category = category.ToUpper();
        newBook.GenerateISBN();

        Catalog.Add(newBook);

        File.WriteAllText("library.json", JsonSerializer.Serialize(Catalog));
    }

    public static Book SelectBook(string inputString)
    {
        var selection = Catalog.Where(B => B.Title == inputString.ToUpper());

        if (selection.Count() == 0)
        {
            selection = Catalog.Where(B => B.Isbn13 == inputString.ToUpper());
        }

        if (selection.Count() == 0)
        {
            throw new ArgumentException("Book not found");
        }

        return selection.First();            
    }

    public static void RemoveBook(Book inputBook)
    {
        var result = Catalog
            .Where(b => b.Isbn13 != inputBook.Isbn13);

        List<Book> newCatalog =result.ToList();

        if (Catalog.Count > newCatalog.Count)
        {
            Catalog = newCatalog;
            File.WriteAllText("library.json", JsonSerializer.Serialize(Catalog));
        }
    }

    public static void PrintCatalog()
    {
        for (int i=0; i< Catalog.Count;i++)
        {
            Console.WriteLine($"{i+1}. {Catalog[i].ToString()}");
        }
    }
}
