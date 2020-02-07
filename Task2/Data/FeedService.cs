using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Task2.Data
{
    public class UpdatedFeedsEventArgs : EventArgs
    {
        public UpdatedFeedsEventArgs(List<Feed> newFeeds)
        {
            this.newFeeds = newFeeds;
        }

        public List<Feed> newFeeds { get; }
    }

    public class FeedService
    {
        public delegate void UpdatedFeed(object sender, UpdatedFeedsEventArgs args);

        public event UpdatedFeed OnUpdatedFeeds;

        public FeedService()
        {
            /*var start = TimeSpan.Zero;
            var period = TimeSpan.FromSeconds(15);

            var timer = new Timer((k) =>
            {
                GetFeeds("https://habr.com/ru/rss/interesting/");
            }, null, start, period);*/
        }

        public List<Feed> GetFeeds(string source)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(source);
            XmlElement rootElement = doc.DocumentElement;

            XmlNode channelNode = GetChannelNode(rootElement);

            //OnUpdatedFeeds(this, new UpdatedFeedsEventArgs(GetFeeds(channelNode)));
            return GetFeeds(channelNode);
        }

        private XmlNode GetChannelNode(XmlElement xmlElement)
        {
            if (xmlElement.Name == "channel")
                return xmlElement;

            XmlNode channelNode = xmlElement.SelectSingleNode("channel");

            if (channelNode != null)
                return channelNode;

            throw new Exception();
        }

        private List<Feed> GetFeeds(XmlNode node)
        {
            List<Feed> feeds = new List<Feed>();

            foreach (XmlNode element in node.SelectNodes("item"))
            {
                feeds.Add(GetFeed(element));
            }

            return feeds;
        }

        private Feed GetFeed(XmlNode node)
        {
            Feed feed = new Feed();

            foreach (XmlElement element in node)
            {
                ParseFeed(element, feed);
            }

            return feed;
        }

        private void ParseFeed(XmlElement element, Feed feed)
        {
            if (!Enum.TryParse(element.Name, true, out FeedRssProperties currentProperty))
                return;
            
            switch (currentProperty)
            {
                case FeedRssProperties.Title:
                    feed.Title = element.InnerText;
                    break;
                case FeedRssProperties.Link:
                    feed.SourceLink = element.InnerText;
                    break;
                case FeedRssProperties.Description:
                    feed.Description = element.InnerText;
                    break;
                case FeedRssProperties.PubDate:
                    feed.PublicationTime = DateTime.Parse(element.InnerText);
                    break;
            }
        }
    }
}