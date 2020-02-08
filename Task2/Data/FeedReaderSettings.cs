using System.Collections.Generic;

namespace Task2.Data
{
    public class FeedReaderSettings
    {
        public int RefreshTime { get; set; }
        public List<RssSource> Sources { get; set; } = new List<RssSource>();
        public bool SupportFormatting { get; set; }
    }
}