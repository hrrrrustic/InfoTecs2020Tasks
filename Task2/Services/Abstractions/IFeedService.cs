using System.Collections.Generic;
using Task2.Models;

namespace Task2.Services.Abstractions
{
    public interface IFeedService
    {
        bool ValidSource(string source);
        List<Feed> GetFeeds(IEnumerable<string> sources);
    }
}