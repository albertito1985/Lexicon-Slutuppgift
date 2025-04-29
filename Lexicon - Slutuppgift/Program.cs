using Lexicon_Slutuppgift.Core;
using Lexicon_Slutuppgift.Menus;

namespace Lexicon_Slutuppgift
{
    internal class Program
    {
        static void Main()
        {
                Library.LoadCatalog("library");
                AvailableMenus.mainMenu.MenuInteraction();
        }
    }
}
