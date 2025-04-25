using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexicon_Slutuppgift;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Menus;

public static class MainMenu
{
    public static List<Option> optionList = new(){
        new Option("Add a book", AddBook),
        new Option("Remove a book", RemoveBook),
        new Option("List all books", ListBooks),
        new Option("Search for a book", SearchBook)

    };

    private static void AddBook()
    {
        try
        {
            ConsoleUtils.NewTitle("Add Book");
            string author = ConsoleUtils.Prompt("Author");
            string title = ConsoleUtils.Prompt("Title");
            string cathegory = ConsoleUtils.Prompt("Cathegory");
            Library.AddBook(author, title, cathegory);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
        Menu.message = "Book added";

    }
    private static void RemoveBook()
    {
        AvailableMenus.removeMenu.MenuInteraction();
    }
    private static void ListBooks()
    {
        throw new NotImplementedException();
    }

    private static void SearchBook()
    {
        throw new NotImplementedException();
    }

}
