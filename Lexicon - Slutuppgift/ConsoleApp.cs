using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexicon_Slutuppgift.Core;
using Lexicon_Slutuppgift.Core.Collections;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift;
public class ConsoleApp
{
    BooksHandler library;
    ItemHandler members;
    Menu mainMenu;
    Menu adminMenu;

    List<Option> mainMenuOptions = new(){
        new Option("Add a book", AddBook),
        new Option("Remove a book", RemoveBook),
        new Option("List all books", ListBooks),
        new Option("Search for a book", SearchBook),
        new Option("Loan a book", LoanBook),
        new Option("Admin Area", AdminArea)
    };

    List<Option> adminMenuOptions = new(){
        new Option("Add Dummy library", LoadDummyLibrary),
        new Option("Clear Library", ClearLibrary)
        };

    public void start()
    {
        setUp();
        mainMenu.MenuInteraction();
    }

    public void setUp()
    {
        library = new BooksHandler("library");
        members = new("members");
        mainMenu = new("Main Menu", mainMenuOptions);
        adminMenu = new("Admin Menu", adminMenuOptions);
    }


    //Main Menu
    private void AddBook()
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
            if (ValidationUtils.String(input)) newBook.Name = input;
            else Console.WriteLine("Please enter a Title.");
        } while (newBook.Name == null);
        do
        {
            string input = ConsoleUtils.Prompt("Category").ToUpper();
            if (ValidationUtils.String(input)) newBook.Category = input;
            else Console.WriteLine("Please enter a Category.");
        } while (newBook.Category == null);
        do
        {
            string input = ConsoleUtils.Prompt("ISBN13");
            if (ValidationUtils.String(input) && ValidationUtils.StringLength(input, 13, 13) && ValidationUtils.IsNumber(input)) newBook.IdNr = input;
            else Console.WriteLine("Please enter a 13 digits ISBN.");
        } while (newBook.IdNr == null);


        if (library.Add(newBook)) Menu.message = "Book added";
    }

    private void RemoveBook()
    {
        string input = ConsoleUtils.Prompt("Enter a Tittle or a ISBN to remove");

        if (ValidationUtils.String(input))
        {
            Book selectedBook = ItemCollection.SelectBook(input);
            if (selectedBook != null)
            {
                if (ItemCollection.RemoveBook(selectedBook))
                    Menu.message = $"BOOK REMOVED\nTitle: {selectedBook.Name}, Author: {selectedBook.Author}, ISBN: {selectedBook.IdNr}";
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

    private void ListBooks()
    {
        ConsoleUtils.NewTitle("Book Catalog");

        for (int i = 0; i < ItemCollection.Catalog.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {ItemCollection.Catalog[i].ToString()}\n");
        }

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    private void SearchBook()
    {
        string input = ConsoleUtils.Prompt("Enter a Tittle or a ISBN to search");
        if (ValidationUtils.String(input))
        {
            Book selectedBook = ItemCollection.SelectBook(input);

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

    private void LoanBook()
    {
        string input = ConsoleUtils.Prompt("Enter a ISBN13 number");
        ValidationUtils.String(input);
        if (ItemCollection.LoanBook(input)) Menu.message = "Book loaned successfully";

    }

    private void AdminArea()
    {
        AvailableMenus.adminMenu.MenuInteraction();
    }


    //Admin Menu
    private void ClearLibrary()
    {
        ItemCollection.ClearCatalog();
        Menu.message = "Library Cleared";
    }

    private void LoadDummyLibrary()
    {
        ItemCollection.LoadCatalog("dummy");
        ItemCollection.PushCatalogToMain();
        Menu.message = "Dummy Library Loaded";
    }
}
