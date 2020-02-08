using System.Collections.Generic;
using Task2.Models;

namespace Task2.Data
{
    public static class FeedReaderSettings
    {
        public static int RefreshTime { get; set; }

        public static List<RssSource> Sources { get; set; } = new List<RssSource>
            {new RssSource("https://habr.com/ru/rss/all/all/", true)};

        public static bool SupportFormatting { get; set; }
    }
}