using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexicon_Slutuppgift.Core.Collections
{
    public interface IIdentifiable
    {
        public string IdNr { get; set; }
        public string Name { get; set; }
    }
}
