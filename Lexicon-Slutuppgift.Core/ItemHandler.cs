using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lexicon_Slutuppgift.Core.Collections;

namespace Lexicon_Slutuppgift.Core;

public class ItemHandler
{
    private string mainCatalogName;
    public LibraryCollection<IIdentifiable> Catalog { get; set; } = new LibraryCollection<IIdentifiable>();
    public ItemHandler(string inputString)
    {
        mainCatalogName = inputString;
        LoadCatalogFile(mainCatalogName);
    }

    public void LoadCatalogFile(string library)
    {
        try
        {
            if (File.Exists($"{library}.json"))
            {
               Catalog = JsonSerializer.Deserialize<LibraryCollection<IIdentifiable>>(File.ReadAllText($"{library}.json"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading the catalog: {ex.Message}");
        }
    }

    public void ClearCatalog()
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

    public bool Add(IIdentifiable newItem)
    {
        try
        {
            PushCatalogToMain();
            Catalog.Add(newItem);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Adding the book: {ex.Message}");
            return false;
        }
    }

    public IIdentifiable Select(string inputString)
    {
        IIdentifiable selection = (IIdentifiable)Catalog.FirstOrDefault(i => i.Name == inputString.ToUpper());
        if (selection == null) selection = Catalog.FirstOrDefault(i => i.IdNr == inputString.ToUpper());
        if (selection == null) return null;
        return selection;
    }

    public bool Remove(IIdentifiable inputBook) //Revisar
    {
        var result = Catalog
            .Where(i => i.IdNr != inputBook.IdNr);

        LibraryCollection<IIdentifiable> newCatalog = (LibraryCollection<IIdentifiable>)result;

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

    public void PushCatalogToMain()
    {
        File.WriteAllText($"{mainCatalogName}.json", JsonSerializer.Serialize(Catalog));
    }

}
