using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Slutuppgift
{
    public class Book
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public bool OnLoan { get; set; } = false;

        public Book(string inputName, string inputTitle, string inputISBN)
        {
            Name = inputName;
            Title = inputTitle;
            ISBN = inputISBN;
        }
    }
}
