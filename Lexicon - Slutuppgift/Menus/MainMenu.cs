using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexicon_Slutuppgift;
using Lexicon_Slutuppgift.Core;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Menus;

public static class MainMenu
{
    public static List<Option> optionList = new(){
        new Option("Add a book", AddBook),
        new Option("Remove a book", RemoveBook),
        new Option("List all books", ListBooks),
        new Option("Search for a book", SearchBook),
        new Option("Loan a book", LoanBook),
        new Option("Clear Library", ClearLibrary),
        new Option("Load dummy library",LoadDummyLibrary)

    };

    private static void AddBook()
    {
        Book newBook = new();
        do
        {
            string input = ConsoleUtils.Prompt("Author").ToUpper();
            if (ValidationUtils.String(input)) newBook.Author = input;
            else Console.WriteLine("Please enter an author.");
        } while (newBook.Author == null);
        do
        {
            string input = ConsoleUtils.Prompt("Title").ToUpper();
            if (ValidationUtils.String(input)) newBook.Title = input;
            else Console.WriteLine("Please enter a Title.");
        } while (newBook.Title == null);
        do
        {
            string input = ConsoleUtils.Prompt("Category").ToUpper();
            if (ValidationUtils.String(input)) newBook.Category = input;
            else Console.WriteLine("Please enter a Category.");
        } while (newBook.Category == null);
        do
        {
            string input = ConsoleUtils.Prompt("ISBN13");
            if (ValidationUtils.String(input) && ValidationUtils.StringLength(input,13,13) && ValidationUtils.IsNumber(input)) newBook.Isbn13 = input;
            else Console.WriteLine("Please enter a 13 digits ISBN.");
        } while (newBook.Isbn13 == null);                   

        
        if(Library.AddBook(newBook)) Menu.message = "Book added";
    }

    private static void RemoveBook()
    {
        string input = ConsoleUtils.Prompt("Enter a Tittle or a ISBN to remove");

        if (ValidationUtils.String(input))
        {
            Book selectedBook = Library.SelectBook(input);
            if (selectedBook != null)
            {
                if(Library.RemoveBook(selectedBook))
                    Menu.message = $"BOOK REMOVED\nTitle: {selectedBook.Title}, Author: {selectedBook.Author}, ISBN: {selectedBook.Isbn13}";
            }
            else
            {
                Menu.message = "Book not found.";
            }
        }
        else
        {
            Menu.message = "Please enter a valid title or ISBN.";
        }
        
    }

    private static void ListBooks()
    {
        ConsoleUtils.NewTitle("Book Catalog");

        for (int i = 0; i < Library.Catalog.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Library.Catalog[i].ToString()}\n");
        }

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private static void SearchBook()
    {
        string input = ConsoleUtils.Prompt("Enter a Tittle or a ISBN to search");
        if (ValidationUtils.String(input))
        {
            Book selectedBook = Library.SelectBook(input);

            if (selectedBook != null)
            {
                ConsoleUtils.NewTitle("Book Found");
                Console.WriteLine(selectedBook.ToString());
                Console.Write("Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Menu.message = $"No book found with the title or ISBN: {input}";
            }
        }
        else
        {
            Menu.message = "Enter a valid Tittle or a ISBN.";
        }
    }

    private static void LoanBook()
    {
        string input = ConsoleUtils.Prompt("Enter a ISBN13 number");
        ValidationUtils.String(input);
        if(Library.LoanBook(input)) Menu.message = "Book loaned successfully";
        
    }

    private static void ClearLibrary()
    {
        Library.ClearCatalog();
        Menu.message = "Library Cleared";
    }

    private static void LoadDummyLibrary()
    {
        Library.LoadCatalog("dummy");
        Library.PushCatalogToMain();
        Menu.message = "Dummy Library Loaded";
    }
}
