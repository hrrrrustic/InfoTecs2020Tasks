using System;

namespace Task2.Models
{
    public class Feed
    {
        public Guid FeedId { get; } = Guid.NewGuid();
        public string Description { get; set; }
        public string SourceLink { get; set; }
        public string Title { get; set; }
        public DateTime? PublicationTime { get; set; }

        public bool IsFilled()
        {
            return !(Description == null || SourceLink == null || Title == null || PublicationTime == null);
        }
    }
}