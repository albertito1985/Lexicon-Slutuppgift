using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Slutuppgift.Core.Collections
{
    public class LibraryCollection<T> : IEnumerable<T>
    {
        T[] collection;

        public int Count { get; private set; }

        public LibraryCollection()
        {
            collection = new T[2];
        }

        public void Add(T item)
        {
            if (Count == collection.Length)
                Array.Resize(ref collection, Count * 2);

            collection[Count++] = item;
        }

        public void Clear()
        {
            collection = new T[2];
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return collection[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
