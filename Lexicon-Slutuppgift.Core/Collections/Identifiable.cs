using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slutuppgift.Utils;

namespace Lexicon_Slutuppgift.Core.Collections
{
    public class Identifiable
    {
        private string name;
        private string idNr;
        public string Name
        {
            get => name;
            set
            {
                if (ValidationUtils.String(value))
                {
                    name = value;
                }
                ;
            }
        }
        public virtual string IdNr
        {
            get => idNr;
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
    }
}
