using System.Collections.Generic;
using Task2.Models;

namespace Task2.Data
{
    public class FeedReaderSettings
    {
        public int RefreshTime { get; set; }
        public List<RssSource> Sources { get; set; } = new List<RssSource>{ new RssSource("https://habr.com/ru/rss/all/all/", true) };
        public bool SupportFormatting { get; set; }
    }
}