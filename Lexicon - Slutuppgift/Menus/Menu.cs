using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Menus
{
    public class Menu
    {
        public static string message { get; set; }
        private List<Option> MenuOptionsList { get; set; }
        private string menuName;
        public int Length { get; private set; }

        internal bool Quit { get; set; } = false;

        public Menu(string inputName, List<Option> optionsList)
        {
            menuName = inputName;
            MenuOptionsList = optionsList;
            Option.InputCount = 0;
            Length = MenuOptionsList.Count;
            MenuOptionsList.Add(new Option("Exit", ExitMenu) { Command = "E" });
        }

        private void ExitMenu()
        {
            Quit = true;
        }

        public void MenuInteraction()
        {
            do
            {
                Console.Clear();
                if (message != null)
                {
                    Console.WriteLine(message);
                    message = null;
                }

                try
                {
                    ShowMenu();
                    string choice = PromptChoice();
                    Option choosedOption = null;
                    if (ValidateOption(choice, ref choosedOption))
                    {
                        choosedOption.Handler();
                    }
                }
                catch (ArgumentException ex)
                {
                    message = $"{ex.Message}";
                }


            } while (!Quit);

        }
        public void ShowMenu()
        {
            int nameLength = menuName.Length;
            var titleInitial = (35 - nameLength) / 2;
            string tittleString = "";

            for (int i = 0; i < 35 - nameLength; i++)
            {
                if (i == titleInitial)
                {
                    tittleString += menuName;
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

            foreach (Option option in MenuOptionsList)
            {
                Console.WriteLine(option.ToString());

            }

            Console.WriteLine("-----------------------------------");
        }

        public string PromptChoice()
        {
            Console.WriteLine("Type the number of the option you want to follow.");
            Console.Write("Option: ");
            string input = Console.ReadLine();
            if (ValidationUtils.String(input)) return input;
            else return "";
        }

        public bool ValidateOption(string choice, ref Option choosedOption)
        {
            string StringChoice = choice.ToUpper();
            foreach (Option option in MenuOptionsList)
            {
                if (option.Command == StringChoice)
                {
                    choosedOption = option;
                    return true;
                }
            }
            throw new ArgumentException("Your input is invalid.");
        }

    }
}
