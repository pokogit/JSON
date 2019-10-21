using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON.Classes
{
    public class Kreditkarte
    {
        public string Herausgeber { get; set; }
        public string Nummer { get; set; }
        public double Deckung { get; set; }
        public string Waehrung { get; set; }
        public Inhaber Inhaber { get; set; }
    }
}
