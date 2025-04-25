using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Slutuppgift.Core
{
    public class Catalog
    {
        public List<Book> Books { get; set; } = new();

        public void Add(Book book)
        {
            Books.Add(book);
        }

        public void Clear()
        {
            Books = new();
        }
    }
}
