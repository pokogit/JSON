using System;
using System.Collections.Generic;

namespace JSON
{
    class Movie
    {
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IList<string> Genres { get; set; }
    }
}

