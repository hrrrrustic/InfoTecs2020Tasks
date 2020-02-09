using System.Collections.Generic;
using Task2.Models;

namespace Task2.Data
{
    public static class FeedReaderSettings
    {
        public static uint RefreshTime { get; set; }

        public static List<RssSource> Sources { get; set; }

        public static bool SupportFormatting { get; set; }
    }
}