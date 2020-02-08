namespace Task2.Models
{
    public class RssSource
    {
        public RssSource(string link, bool active)
        {
            Link = link;
            Active = active;
        }

        public string Link { get; set; }
        public bool Active { get; set; }
    }
}