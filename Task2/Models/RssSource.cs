using System;

namespace Task2.Models
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
