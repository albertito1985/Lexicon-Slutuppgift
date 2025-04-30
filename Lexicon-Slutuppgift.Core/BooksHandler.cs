using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexicon_Slutuppgift.Core.Collections;

namespace Lexicon_Slutuppgift.Core
{
    public class BooksHandler : ItemHandler<Book>
    {
        public BooksHandler(string inputString) : base(inputString)
        {
        }

        public bool Loan(string inputISBN13)
        {
            Identifiable loanBook = Catalog.FirstOrDefault(b => b.IdNr == inputISBN13);
            Book loanBookBook = (Book)loanBook;
            if (loanBookBook == null) return false;
            loanBookBook.OnLoan = true;

            try
            {
                PushCatalogToMain();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating the catalog: {ex.Message}");
                loanBookBook.OnLoan = false;
                return false;
            }
        }
    }
}
