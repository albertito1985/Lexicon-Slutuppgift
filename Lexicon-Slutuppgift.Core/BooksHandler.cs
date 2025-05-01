using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Lexicon_Slutuppgift.Core.Collections;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Core
{
    public class BooksHandler : ItemHandler<Book>
    {
        public BooksHandler(string inputString) : base(inputString)
        {
        }

        public bool Loan(string inputISBN13)
        {
            Identification loanBook = Catalog.FirstOrDefault(b => b.IdNr == inputISBN13);
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

        public bool Return(string inputISBN13)
        {
            Identification ReturnBook = Catalog.FirstOrDefault(b => b.IdNr == inputISBN13);
            Book returnBookBook = (Book)ReturnBook;
            if (returnBookBook == null) return false;
            returnBookBook.OnLoan = false;

            try
            {
                PushCatalogToMain();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating the catalog: {ex.Message}");
                returnBookBook.OnLoan = true;
                return false;
            }
        }
        public override bool Add(Book newItem)
        {
            if (newItem.Author == null) return false;
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
                Console.WriteLine($"Error adding the book: {ex.Message}");
                return false;
            }
        }

        public string GenerateBorrowedReport(string path)
        {
            var loaned = Catalog.Where(i => i.OnLoan == true).ToList();
            List<Book> loanedBooks = (List<Book>)loaned;
            if (loanedBooks.Count == 0)
            {
                return null;
            }
            else
            {
                string fileExport = "LOANED BOOKS\n";

                for (int i = 0; i < loanedBooks.Count; i++)
                {
                    string newBook = $"\n{i + 1}. {loanedBooks[i].ToString()}\n";
                    fileExport += newBook;
                }
                return fileExport;
                
            }
            
        }
    }
}

    
