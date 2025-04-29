using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift
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
                    if (ValidationUtils.String(choice))
                    {
                        Option choosedOption = null;
                        if (ValidateOption(choice, ref choosedOption))
                        {
                            choosedOption.Handler();
                        }
                        else
                        {
                            message = "Your choice is out of range.";
                        }
                    }
                    else
                    {
                        message = "Enter a valid input.";
                    }

                        
                }
                catch (Exception ex)
                {
                    message = $"{ex.Message}";
                }


            } while (!Quit);

        }

        public void ShowMenu()
        {
            GenerateTitle(menuName);

            foreach (Option option in MenuOptionsList)
            {
                Console.WriteLine(option.ToString());

            }

            Console.WriteLine("-----------------------------------");
        }

        public void GenerateTitle(string title)
        {
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


        public string PromptChoice()
        {
            Console.WriteLine("Type the number of the option you want to follow.");
            Console.Write("Option: ");
            return Console.ReadLine();
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
            return false;
        }

    }
}
