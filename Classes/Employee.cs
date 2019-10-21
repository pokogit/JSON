using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    public class Employee
    {
        public string Name { get; set; }
        public Employee Manager { get; set; }
        public bool ShouldSerializeManager()
        {
            return (Manager != this);
        }
    }
}
