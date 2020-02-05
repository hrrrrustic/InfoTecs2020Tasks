using System;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Task2.Data
{
    public class Feed
    {
        public string Description { get; set; }
        public string SourceLink { get; set; }
        public string Title { get; set; }
        public DateTime PublicationTime { get; set; }
    }
}