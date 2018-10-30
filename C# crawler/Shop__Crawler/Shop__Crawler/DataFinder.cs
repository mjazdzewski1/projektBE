using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abot.Poco;

namespace Shop__Crawler
{
    class DataFinder
    {
        public static string FindNameAndPrice(CrawledPage crawledPage)
        {
            var result = "";
            var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-prices");
            var a = x?.ChildNodes.FindFirst("div");
            var b = a?.ChildNodes.FindFirst("span");
            var c = b?.ChildNodes.FindFirst("span");
            var price = c?.ChildNodes.FindFirst("span")?.InnerText;
            if (price != null)
            {
                var name = crawledPage.HtmlDocument.GetElementbyId("p-inner-name");
                var nameFormatted = name?.ChildNodes.FindFirst("h1").InnerText;
                nameFormatted = nameFormatted?.Replace('\n', ' ');
                nameFormatted = nameFormatted?.Trim();
                result += $"{nameFormatted} + {price}";
            }

            return result;
        }
    }
}
