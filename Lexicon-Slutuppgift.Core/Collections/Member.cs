using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Core.Collections
{
    public class Member : Identifiable
    {
        private string idNr;
        public string IdNr
        {
            get;
            set
            {
                if (ValidationUtils.String(value) &&
                    ValidationUtils.StringLength(value, 13, 13) &&
                    ValidationUtils.IsNumber(value))
                {
                    idNr = value;
                }
                ;
            }
        }
        public string Name { get ; set=> ValidationUtils.String(value); }

        Member(string inputName)
        {
            Name = inputName;
        }
    }
}
