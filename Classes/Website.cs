using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    public class Website
    {
        public string Url { get; set; }
        private Website() { }
        public Website(Website website)
        {
            if (website == null)
            {
                throw new ArgumentNullException(nameof(website));
            }
            Url = website.Url;
        }
    }
}
