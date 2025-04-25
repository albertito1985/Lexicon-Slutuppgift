using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutuppgift.Utils;

public static class ConsoleUtils
{
    public static string Prompt(string InputString)
    {
        Console.Write($"{InputString}: ");
        return Input();
    }

    public static string Ask(string InputString)
    {
        Console.WriteLine($"{InputString}");
        return Input();
    }
    public static string Input()
    {
        string input = Console.ReadLine();
        ValidationUtils.String(input);
        return input;
    }

    public static void NewTitle(string title)
    {
        Console.Clear();
        int nameLength = title.Length;
        var titleInitial = (35 - nameLength) / 2;
        string tittleString = "";

        for (int i = 0; i < 35 - nameLength; i++)
        {
            if (i == titleInitial)
            {
                tittleString += title;
            }
            else
            {
                tittleString += "-";
            }
        }
        if (nameLength % 2 != 0)
        {
            tittleString += "-";
        }
        Console.WriteLine(tittleString);
    }
}
