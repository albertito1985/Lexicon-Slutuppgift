using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lexicon_Slutuppgift.Core.Collections;

namespace Lexicon_Slutuppgift.Core;

public class ItemHandler<T> where T : Identification
{
    private string mainCatalogName;
    public List<T> Catalog { get; set; } = new List<T>();
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
               Catalog = JsonSerializer.Deserialize<List<T>>(File.ReadAllText($"{library}.json"));
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

    public virtual bool Add(T newItem)
    {
        if (newItem.Name == null) return false;
        if (newItem.IdNr == null) return false;
        try
        {
            Catalog.Add(newItem);
            PushCatalogToMain();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Adding the book: {ex.Message}");
            return false;
        }
    }

    public T Select(string inputString)
    {
        T selection = (T)Catalog.FirstOrDefault(i => i.Name == inputString.ToUpper());
        if (selection == null) selection = Catalog.FirstOrDefault(i => i.IdNr == inputString.ToUpper());
        if (selection == null) return null;
        return selection;
    }

    public bool Remove(T inputBook) //Revisar
    {
        var result = Catalog
            .Where(i => i.IdNr != inputBook.IdNr);

        List<T> newCatalog = result.ToList();

        if (Catalog.Count > newCatalog.Count)
        {
            try
            {
                Catalog = newCatalog;
                PushCatalogToMain();
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
