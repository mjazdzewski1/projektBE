using System;
using System.Text.RegularExpressions;
using System.Xml;
using Abot.Poco;
using HtmlAgilityPack;
using Shop__Crawler.Models;

namespace Shop__Crawler
{
    public class KomputronikDataExtractor : IDataExtractor
    {
        public ExportedCsvModel AddDescription(CrawledPage crawledPage, ExportedCsvModel model)
        {
            var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-features");
            model.Description = x.InnerHtml.Replace('\n', ' ').Replace("Zobacz wszystkie cechy", "");
            return model;
        }

        public ExportedCsvModel AddCategories(CrawledPage crawledPage, ExportedCsvModel model)
        {
            try
            {
                var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-breadcrumb");
                var categoriesText = x.InnerText.Remove(x.InnerText.IndexOf('{'));
                categoriesText = categoriesText.Replace('\n', ',');

                categoriesText = Regex.Replace(categoriesText, @"\s+", "");
                var cleaned = false;
                while (!cleaned)
                {
                    var old = categoriesText;
                    categoriesText = categoriesText.Replace(",,", ",");
                    if (categoriesText[0] == ',')
                        categoriesText = categoriesText.Remove(0, 1);
                    if (categoriesText[categoriesText.Length - 1] == ',')
                        categoriesText = categoriesText.Remove(categoriesText.Length - 1);
                    if (old == categoriesText)
                        cleaned = true;
                }

                categoriesText = categoriesText.Remove(0, categoriesText.IndexOf(',') + 1);
                categoriesText = categoriesText.Remove(categoriesText.LastIndexOf(','));

                var mainCategory = categoriesText.Remove(0, categoriesText.LastIndexOf(',') + 1);
                categoriesText = categoriesText.Remove(categoriesText.LastIndexOf(','));
                categoriesText = mainCategory + "," + categoriesText;

                model.Category = categoriesText;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                model.Category = "";
            }

            return model;
        }

        public ExportedCsvModel FormatPrice(ExportedCsvModel model)
        {
            model.Price = model.Price.Replace('z', ' ');
            model.Price = model.Price.Replace('ł', ' ');
            model.Price = model.Price.Trim();
            model.Price = Regex.Replace(model.Price, @"\s+", "");

            if(!double.TryParse(model.Price, out _))
                model.Price = "119.90";

            return model;
        }

        public ExportedCsvModel AddPrice(CrawledPage crawledPage, ExportedCsvModel model)
        {
            var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-prices");
            var a = x?.ChildNodes.FindFirst("div");
            var b = a?.ChildNodes.FindFirst("span");
            var c = b?.ChildNodes.FindFirst("span");
            var price = c?.ChildNodes.FindFirst("span")?.InnerText;

            if (price != null)
            {
                if (price == "90")
                    price = GetPriceWithCents(c);
                model.Price = price;
                return model;
            }

            if (c?.InnerText.Contains("zł") ?? false)
            {
                var priceText = c.InnerText;
                if (priceText.Contains("Cena"))
                {
                    priceText = priceText.Replace("Cena internetowa", "");
                }

                model.Price = priceText.Replace('\n', ' ').Trim();

                return model;
            }

            return model;
        }
        public ExportedCsvModel AddName(CrawledPage crawledPage, ExportedCsvModel model)
        {
            var name = crawledPage.HtmlDocument.GetElementbyId("p-inner-name");
            var nameFormatted = name?.ChildNodes.FindFirst("h1").InnerText;
            nameFormatted = nameFormatted?.Replace('\n', ' ');
            nameFormatted = nameFormatted?.Trim();
            model.Name = nameFormatted;

            return model;
        }

        public ExportedCsvModel AddImage(CrawledPage crawledPage, ExportedCsvModel model)
        {
            var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-gallery");
            var a = x?.ChildNodes.FindFirst("ktr-gallery");
            var b = a?.ChildNodes.FindFirst("div");
            var c = b?.ChildNodes.FindFirst("div").InnerHtml;

            if (c != null)
            {
                var pFrom = c.IndexOf("static");
                var pTo = c.IndexOf(".png") + ".png".Length;

                if (pFrom < 1 || pTo < 1)
                    return model;
                model.ImageUrl = "https://" + c.Substring(pFrom, pTo - pFrom);
            }

            return model;
        }

        public ExportedCsvModel AddCategory(CrawledPage crawledPage, ExportedCsvModel model)
        {
            var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-gallery");
            var a = x?.ChildNodes.FindFirst("ktr-gallery");
            var b = a?.ChildNodes.FindFirst("div");
            var c = b?.ChildNodes.FindFirst("div").InnerHtml;

            model.Category = "CATEGORY";

            return model;
        }

        private string GetPriceWithCents(HtmlNode parentNode)
        {
            var zlots = parentNode.ChildNodes.FindFirst("#text").InnerText;
            var groszes = parentNode.ChildNodes.FindFirst("span").InnerText;
            zlots = zlots?.Replace('\n', ' ');
            zlots = zlots?.Trim();
            return $"{zlots}.{groszes}zł";
        }

        public bool IsNotProductPage(CrawledPage page)
        {
            return page.Uri.ToString().Contains("category") || page.Uri.ToString().Contains("informacje")
                                                            || page.Uri.ToString().Contains("search-filter") || page.Uri.ToString().Contains("customer") || page.Uri.ToString().Contains("productVariantGroup");
        }
    }
}