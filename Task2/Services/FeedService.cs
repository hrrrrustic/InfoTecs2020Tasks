﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.VisualBasic.CompilerServices;
using Task2.Data;
using Task2.Models;
using Task2.Services.Abstractions;

namespace Task2.Services
{
    public class FeedService : IFeedService
    {
        public List<Feed> GetFeeds(IEnumerable<string> sources)
        {
            try
            {
                return sources.Where(ValidSource).Select(GetFeeds).SelectMany(k => k).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool ValidSource(string source)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(source);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
            if (xmlElement.Name == FeedRssProperties.Channel.ToLowerString())
                return xmlElement;

            XmlNode channelNode = xmlElement.SelectSingleNode(FeedRssProperties.Channel.ToLowerString());

            if (channelNode != null)
                return channelNode;

            throw new NotSupportedException();
        }

        private List<Feed> GetFeeds(XmlNode node)
        {
            var feeds = new List<Feed>();

            XmlNodeList itemNodes = node.SelectNodes(FeedRssProperties.Item.ToLowerString());

            if (itemNodes.Count == 0)
                throw new NotSupportedException();

            foreach (XmlNode itemNode in itemNodes)
            {
                feeds.Add(GetFeed(itemNode));

            }

            return feeds;
        }

        private Feed GetFeed(XmlNode node)
        {
            Feed feed = new Feed();

            foreach (XmlElement element in node)
            {
                ParseFeed(element, feed);
                if (feed.IsFilled())
                    break;
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