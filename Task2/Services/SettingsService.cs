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

        public RssSource RemoveSource(string source)
        {
            int index = FeedReaderSettings.Sources.FindIndex(k => k.Link == source);

            if (index == -1)
                return null;

            RssSource currentSource = FeedReaderSettings.Sources[index];
            FeedReaderSettings.Sources.Remove(currentSource);

            return currentSource;
        }

        public RssSource ChangeSourceActivity(string source)
        {
            int index = FeedReaderSettings.Sources.FindIndex(k => k.Link == source);

            if(index == -1)
                return null;

            RssSource currentSource = FeedReaderSettings.Sources[index];
            currentSource.Active = !currentSource.Active;

            return currentSource;
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