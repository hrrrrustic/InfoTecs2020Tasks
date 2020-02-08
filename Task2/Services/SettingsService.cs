using System;
using System.Linq;
using Task2.Data;
using Task2.Models;
using Task2.Services.Abstractions;

namespace Task2.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly IFeedService _feedService;

        public SettingsService(IFeedService feedService)
        {
            _feedService = feedService;
        }

        public void RemoveSource(string source)
        {
            int index = FeedReaderSettings.Sources.FindIndex(k => k.Link == source);
            FeedReaderSettings.Sources.RemoveAt(index);
        }

        public void ChangeSourceActivity(string source)
        {
            int ind = FeedReaderSettings.Sources.FindIndex(k => k.Link == source);
            FeedReaderSettings.Sources[ind].Active = !FeedReaderSettings.Sources[ind].Active;
        }

        public RssSource AddNewLink(string source)
        {
            if (string.IsNullOrWhiteSpace(source) ||
                FeedReaderSettings.Sources.SingleOrDefault(k => k.Link == source) != null)
                return null;

            if (!_feedService.ValidSource(source))
                return null;

            RssSource newSource = new RssSource(source, true);
            FeedReaderSettings.Sources.Add(newSource);

            return newSource;
        }
    }
}