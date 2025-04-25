using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Slutuppgift.Menus;

public class Option
{
    public static int InputCount { get; set; } = 0;
    public string Command { get; set; }
    public int ID { get; set; }
    public string Description { get; set; }
    public Action Handler { get; set; }

    public Option(string inputDescription, Action handler)
    {
        ID = InputCount;
        Command = InputCount.ToString();
        Description = inputDescription;
        Handler = handler;
        InputCount++;
    }
    public override string ToString()
    {
        return $"{Command}: {Description}";
    }
}
