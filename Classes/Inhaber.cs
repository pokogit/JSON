using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON.Classes
{
    public class Inhaber
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public bool Maennlich { get; set; }
        public string[] Hobbys { get; set; }
        public int Alter { get; set; }
        public int[] Kinder { get; set; }
        public int Partner { get; set; }
    }
}
