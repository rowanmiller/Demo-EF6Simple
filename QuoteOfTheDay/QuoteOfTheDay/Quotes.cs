using QuoteOfTheDay.Properties;
using System;

namespace QuoteOfTheDay
{
    public static class Quotes
    {
        public static string GetQuote()
        {
            var index = new Random().Next(1, 4);
            var quote = Resources.ResourceManager.GetString("Quote" + index);
            return quote;
        }
    }
}
