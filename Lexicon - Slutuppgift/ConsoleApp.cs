﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lexicon_Slutuppgift.Core;
using Lexicon_Slutuppgift.Core.Collections;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift;
public class ConsoleApp
{
    BooksHandler library;
    MembersHandler members;
    Menu mainMenu;
    Menu adminMenu;
    Menu catalogMenu;
    Menu membersMenu;
    Menu configurationMenu;
    List<string> history;
    Dictionary<string, bool> config;

    public void start()
    {
        setUp();
        mainMenu.MenuInteraction();
    }

    public void setUp()
    {
        try
        {
            library = new("library");
            members = new("members");
            history = new();
            config = new();
            LoadHistory();
            LoadConfiguration();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        List<Option> mainMenuOptions = new(){
            new Option("Book Catalog", BookCatalog),
            new Option("Library Members", LibraryMembers),
            new Option("Admin area", AdminArea),
            new Option("Configuration", Configuration)
        };
        mainMenu = new("Main Menu", mainMenuOptions);

        List<Option> bookCatalogMenuOptions = new(){
            new Option("Add a book", AddBook),
            new Option("Remove a book", RemoveBook),
            new Option("List all books", ListBooks),
            new Option("Search for a book", SearchBook),
            new Option("Loan a book", LoanBook),
            new Option("Return a book", ReturnBook)
        };
        catalogMenu = new("Book Catalog", bookCatalogMenuOptions);

        List<Option> membersMenuOptions = new(){
            new Option("Add a member", AddMember),
            new Option("Remove a member", RemoveMember),
            new Option("List all members", ListMembers),
            new Option("Search for a member", SearchMember)
        };
        membersMenu = new("Members Menu", membersMenuOptions);

        List<Option> adminMenuOptions = new(){
            new Option("Add Dummy library", LoadDummyLibrary),
            new Option("Clear Library", ClearLibrary),
            new Option("Borrowed Books Report", ReportBorrowedDownload)
        };
        adminMenu = new("Admin Menu", adminMenuOptions);

        string historyOnFile = (config.ContainsKey("historyOnFile") && config["historyOnFile"]) ? "ON" : "OFF";
        List < Option > configurationOptions = new(){
            new Option($"Save history on file : {historyOnFile}", SaveHistoryOnFile)
        };
        configurationMenu = new("Configuration", configurationOptions);

    }

    #region Main Menu
    //Main Menu
    public void BookCatalog()
    {
        catalogMenu.MenuInteraction();
    }

    public void LibraryMembers()
    {
        membersMenu.MenuInteraction();
    }

    public void AdminArea()
    {
        adminMenu.MenuInteraction();
    }

    public void Configuration()
    {
        configurationMenu.MenuInteraction();
    }

    #endregion

    #region Book Catalog Menu
    //Catalog Menu
    public void AddBook()
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

        if (library.Add(newBook))
        {
            Menu.message = "Book added";
            history.Add($"Book added: {newBook.IdNr}");
            if (config.ContainsKey("historyOnFile") && config["historyOnFile"]) SaveHistory();
        }
        
    }

    public void RemoveBook()
    {
        string input = ConsoleUtils.Prompt("Enter a Tittle or a ISBN to remove");

        if (ValidationUtils.String(input))
        {
            Book selectedBook = library.Select(input);
            if (selectedBook != null)
            {
                if (library.Remove(selectedBook)) 
                {
                    Menu.message = $"BOOK REMOVED\nTitle: {selectedBook.Name}, Author: {selectedBook.Author}, ISBN: {selectedBook.IdNr}";
                    history.Add($"Book removed: {selectedBook.IdNr}");
                    if (config.ContainsKey("historyOnFile") && config["historyOnFile"]) SaveHistory();
                }
                    
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

    public void ListBooks()
    {
        ConsoleUtils.NewTitle("Book Catalog");

        var catalogList = library.Catalog.ToList();

        for (int i = 0; i < catalogList.Count; i++)
        {
            Book book = (Book)catalogList[i];
            Console.WriteLine($"{i + 1}. {book.ToString()}\n");
        }

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public void SearchBook()
    {
        string input = ConsoleUtils.Prompt("Enter a Tittle or a ISBN to search");
        if (ValidationUtils.String(input))
        {
            Identification selectedBook = library.Select(input);

            if (selectedBook != null)
            {
                ConsoleUtils.NewTitle("Book Found");
                Console.WriteLine($"\n{selectedBook.ToString()}\n");
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

    public void LoanBook()
    {
        string input = ConsoleUtils.Prompt("Enter a ISBN13 number");
        ValidationUtils.String(input);
        if (library.Loan(input))
        {
            history.Add($"Book loaned: {input}");
            if (config.ContainsKey("historyOnFile") && config["historyOnFile"]) SaveHistory();
            Menu.message = "Book loaned successfully";
        } 

    }

    public void ReturnBook()
    {
        string input = ConsoleUtils.Prompt("Enter a ISBN13 number");
        ValidationUtils.String(input);
        if (library.Return(input))
        {
            history.Add($"Book returned: {input}");
            if (config.ContainsKey("historyOnFile") && config["historyOnFile"]) SaveHistory();
            Menu.message = "Book returned successfully";
        }
    }

    #endregion

    #region Members Menu
    //Members Menu

    public void AddMember()
    {
        Member newMember = new();
        do
        {
            string input = ConsoleUtils.Prompt("Name").ToUpper();
            if (ValidationUtils.String(input)) newMember.Name = input;
            else Console.WriteLine("Please enter an name.");
        } while (newMember.Name == null);
        do
        {
            string input = ConsoleUtils.Prompt("Phonenumber").ToUpper();
            if (ValidationUtils.String(input)) newMember.PhoneNumber = input;
            else Console.WriteLine("Please enter a phonenumber.");
        } while (newMember.PhoneNumber == null);
        do
        {
            string input = ConsoleUtils.Prompt("Address").ToUpper();
            if (ValidationUtils.String(input)) newMember.Address = input;
            else Console.WriteLine("Please enter an address.");
        } while (newMember.Address == null);


        if (members.Add(newMember))
        {
            history.Add($"Member added: {newMember.IdNr}");
            if (config.ContainsKey("historyOnFile") && config["historyOnFile"]) SaveHistory();
            Menu.message = "Member added";
        } 
    }

    public void RemoveMember()
    {
        string input = ConsoleUtils.Prompt("Enter a member number to remove");

        if (ValidationUtils.String(input))
        {
            Member selectedMember = members.Select(input);
            if (selectedMember != null)
            {
                if (members.Remove(selectedMember))
                {
                    history.Add($"Member removed: {selectedMember.IdNr}");
                    if (config.ContainsKey("historyOnFile") && config["historyOnFile"]) SaveHistory();
                    Menu.message = $"MEMBER REMOVED\nTitle: {selectedMember.Name}, Number: {selectedMember.IdNr}";
                }
                    
            }
            else
            {
                Menu.message = "Member not found.";
            }
        }
        else
        {
            Menu.message = "Please enter a valid member number.";
        }
    }

    public void ListMembers()
    {
        ConsoleUtils.NewTitle("Member Catalog");

        var catalogList = members.Catalog.ToList();

        for (int i = 0; i < catalogList.Count; i++)
        {
            Member member = (Member)catalogList[i];
            Console.WriteLine($"{i + 1}. {member.ToString()}\n");
        }

        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public void SearchMember()
    {
        string input = ConsoleUtils.Prompt("Enter a Namme or a member number to search");
        if (ValidationUtils.String(input))
        {
            Identification selectedMember = members.Select(input);

            if (selectedMember != null)
            {
                ConsoleUtils.NewTitle("Member Found");
                Console.WriteLine($"\n{selectedMember.ToString()}\n");
                Console.Write("Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Menu.message = $"No member found with the name or member number: {input}";
            }
        }
        else
        {
            Menu.message = "Enter a valid Name or member number.";
        }
    }

    #endregion

    #region Admin Menu
    //Admin Menu
    public void ClearLibrary()
    {
        library.ClearCatalog();
        Menu.message = "Library Cleared";
    }

    public void LoadDummyLibrary()
    {
        library.LoadCatalogFile("dummy");
        library.PushCatalogToMain();
        Menu.message = "Dummy Library Loaded";
    }

    public void ReportBorrowedDownload()
    {
        string pathString = GenerateNameToFile("BorrowedBooksReport");
        string fileExport = library.GenerateBorrowedReport(pathString);
        if(fileExport!= null)
        {
            File.WriteAllText(pathString, fileExport);
            Menu.message = $"Report generated\nYou can find your new report at {pathString}";
        }
        else
        {
            Menu.message = "There are no books on loan.";

        }
        
    }

    #endregion

    #region Configuration Menu

    public void SaveHistoryOnFile()
    {
        if (config.ContainsKey("historyOnFile"))
        {
            config["historyOnFile"] = !config["historyOnFile"];

        }
        else
        {
            config.Add("historyOnFile", true);
        }
        SaveConfiguration();

        Menu.message=$"History on file: {config["historyOnFile"]}.";
        string historyOnFile = (config.ContainsKey("historyOnFile") && config["historyOnFile"]) ? "ON" : "OFF";
        List<Option> configurationOptions = new(){
            new Option($"Save history on file : {historyOnFile}", SaveHistoryOnFile)
        };
        configurationMenu = new("Configuration", configurationOptions);
    }

    #endregion

    #region generalFunctions

    public void LoadHistory()
    {
        if (File.Exists("history.txt"))
        {
            List<string> incommingHistory = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("history.txt"));
            history = incommingHistory;
        }
        else
        {
            Console.WriteLine("No history found.");
        }
    }

    public void SaveHistory()
    {
        if (history != null)
        {
            File.WriteAllText("history.txt", JsonSerializer.Serialize(history));
        }
        else
        {
            Console.WriteLine("No history to save.");
        }
    }

    public void LoadConfiguration()
    {
        if (File.Exists("configuration.txt"))
        {
            Dictionary<string, bool> incommingHistory = JsonSerializer.Deserialize<Dictionary<string, bool>>(File.ReadAllText("configuration.txt"));
            config = incommingHistory;
        }
        else
        {
            Console.WriteLine("No configuration found.");
        }
    }

    public void SaveConfiguration()
    {
        if (config != null)
        {
            File.WriteAllText("configuration.txt", JsonSerializer.Serialize(config));
        }
        else
        {
            Console.WriteLine("No configuration to save.");
        }
    }

    public string GenerateNameToFile(string fileName)
    {
        bool outVariable = true;
        int count = 0;

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string pathString;
        do
        {
            string number = (count == 0) ? "" : count.ToString();
            pathString = $"{documentsPath}\\{fileName}{number}.txt";
            if (!File.Exists(pathString)) outVariable = false;
            else count++;
        }
        while (outVariable);

        return pathString;
    }

    #endregion
}
