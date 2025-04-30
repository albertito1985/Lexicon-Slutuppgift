using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Core.Collections
{
    public class Member : Identification
    {
        public string Address { get ; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\n" +
                   $"Address: {Address}\n" +
                   $"Phone Number: {PhoneNumber}\n" +
                   $"Member ID: {IdNr}";
            ;
        }
    }
}
