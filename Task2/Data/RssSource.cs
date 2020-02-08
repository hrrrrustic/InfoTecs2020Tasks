using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.Data
{
    public class RssSource
    {
        public string Link { get; set; }
        public bool Active { get; set; }

        public RssSource(string link, bool active)
        {
            Link = link;
            Active = active;
        }
    }
}
