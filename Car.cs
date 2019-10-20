using System;
using static System.Net.Mime.MediaTypeNames;

namespace JSON
{
    class Car
    {
        public DateTime ExpirationDate { get; set; }
        public string Name { get; set; }
        public string[] Sizes { get; set; }
        public bool HasWheels { get; set; }
        public decimal Price { get; set; }
        public double NumberOfKilometers { get; set; }
    }
}
