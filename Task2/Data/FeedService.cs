using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace Task2.Data
{
    public class FeedService
    {
        public List<Feed> GetFeeds(string source)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(source);
            XmlElement rootElement = doc.DocumentElement;

            XmlNode channelNode = GetChannelNode(rootElement);

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
                switch (element.Name)
                {
                    case "title":
                        feed.Title = element.InnerText;
                        break;
                    case "link":
                        feed.SourceLink = element.InnerText;
                        break;
                    case "description":
                        feed.Description = element.InnerText;
                        break;
                    case "pubDate":
                        feed.PublicationTime = DateTime.Parse(element.InnerText);
                        break;
                }
            }

            return feed;
        }
    }
}