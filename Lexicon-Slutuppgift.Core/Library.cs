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
    static string mainCatalogName = "library";
    public static List<Book> Catalog { get; set; } = new();
    public static void LoadCatalog(string library)
    {
        try
        {
            if (File.Exists($"{library}.json"))
            {
                List<Book> loadingCatalog = JsonSerializer.Deserialize<List<Book>>(File.ReadAllText($"{library}.json"));
                Catalog = loadingCatalog;
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error loading the catalog: {ex.Message}");
        }
        
    }

    public static void ClearCatalog()
    {
        try
        {
            if (File.Exists($"{mainCatalogName}.json"))
            {
                File.Delete($"{mainCatalogName}.json");
                Catalog.Clear();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing the catalog: {ex.Message}");
        }
    }

    public static bool AddBook(Book newBook)
    {
        if (newBook.Author == null) return false;
        if (newBook.Title == null) return false;
        if (newBook.Isbn13 == null) return false;
        try
        {
            PushCatalogToMain();
            Catalog.Add(newBook);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Adding the book: {ex.Message}");
            return false;
        }
    }

    public static Book SelectBook(string inputString)
    {
        Book selection = Catalog.FirstOrDefault(B => B.Title == inputString.ToUpper());
        if (selection == null) selection = Catalog.FirstOrDefault(B => B.Isbn13 == inputString.ToUpper());
        if (selection == null) return null;
        return selection;
    }

    public static bool RemoveBook(Book inputBook)
    {
        var result = Catalog
            .Where(b => b.Isbn13 != inputBook.Isbn13);

        List<Book> newCatalog =result.ToList();

        if (Catalog.Count > newCatalog.Count)
        {
            try
            {
                PushCatalogToMain();
                Catalog = newCatalog;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Removing the book: {ex.Message}");
                return false;
            }            
        }
        return false;
    }

    public static bool LoanBook(string inputISBN13)
    {
        Book loanBook = Catalog.FirstOrDefault(b => b.Isbn13 == inputISBN13);
        if (loanBook == null) return false;
        loanBook.OnLoan = true;
        try
        {
            PushCatalogToMain();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Updating the catalog: {ex.Message}");
            loanBook.OnLoan = false;
            return false;
        }
    }

    public static void PushCatalogToMain() 
    {
        File.WriteAllText($"{mainCatalogName}.json", JsonSerializer.Serialize(Catalog));
    }
}
