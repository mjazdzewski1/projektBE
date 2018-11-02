using Abot.Poco;
using HtmlAgilityPack;

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
                if (price == "90")
                    price = GetPriceWithCents(c);
                var name = crawledPage.HtmlDocument.GetElementbyId("p-inner-name");
                var nameFormatted = name?.ChildNodes.FindFirst("h1").InnerText;
                nameFormatted = nameFormatted?.Replace('\n', ' ');
                nameFormatted = nameFormatted?.Trim();
                result += $"{nameFormatted}: {price}";
            }

            return result;
        }

        public static string FindImage(CrawledPage crawledPage)
        {
            var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-gallery");
            var a = x?.ChildNodes.FindFirst("ktr-gallery");
            var b = a?.ChildNodes.FindFirst("div");
            var c = b?.ChildNodes.FindFirst("div").InnerHtml;

            var imageUrl = "";

            if (c != null)
            {
                var pFrom = c.IndexOf("static");
                var pTo = c.IndexOf(".png") + ".png".Length;

                if (pFrom < 1 || pTo < 1)
                    return "";
                imageUrl = c.Substring(pFrom, pTo - pFrom);
            }

            return imageUrl;
        }

        private static string GetPriceWithCents(HtmlNode parentNode)
        {
            var zlots = parentNode.ChildNodes.FindFirst("#text").InnerText;
            var groszes = parentNode.ChildNodes.FindFirst("span").InnerText;
            zlots = zlots?.Replace('\n', ' ');
            zlots = zlots?.Trim();
            return $"{zlots}.{groszes}zł";
        }
    }
}
