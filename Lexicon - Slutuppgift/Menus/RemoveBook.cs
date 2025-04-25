using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexicon_Slutuppgift.Core;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Menus;

public class RemoveBook
{
    public static List<Option> optionList = new(){
        new Option("Remove by Title or ISBN13", RemoveBookByString),
        new Option("Clear Library", ClearLibrary)
    };
    
    private static void RemoveBookByString()
    {
        try
        {
            string input = ConsoleUtils.Prompt("Enter a Tittle or a ISBN to remove");
            ValidationUtils.String(input);
            Book selectedBook = Library.SelectBook(input);
            Library.RemoveBook(selectedBook);

            Menu.message = $"BOOK REMOVED\nTitle: {selectedBook.Title}, Author: {selectedBook.Author}, ISBN: {selectedBook.Isbn13}";
        }
        catch(Exception ex)
        {
            Menu.message = $"{ex.Message}";
        }

        
    }

    private static void ClearLibrary()
    {
        Library.ClearLibrary();
        Menu.message = "Library Cleared";
    }
}
