using System.Collections.Generic;

namespace Task2.Data
{
    public class FeedReaderSettings
    {
        public int RefreshTime { get; set; }
        public List<string> SourceLinks { get; set; } = new List<string>() {"1", "2", "3"};
        public bool SupportFormatting { get; set; }
    }
}