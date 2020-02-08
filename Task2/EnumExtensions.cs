using System;
using Task2.Data;

namespace Task2
{
    public static class EnumExtensions
    {
        public static string ToLowerString(this FeedRssProperties property)
        {
            return property.ToString().ToLower();
        }
    }
}