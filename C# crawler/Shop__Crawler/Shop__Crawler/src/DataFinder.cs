﻿using System.Text.RegularExpressions;
using Abot.Poco;
using HtmlAgilityPack;
using Shop__Crawler.Models;

namespace Shop__Crawler
{
    class DataFinder
    {
        public static void Run(CrawledPage page)
        {
            if (page.Uri.ToString().Contains("category") || page.Uri.ToString().Contains("informacje")
                || page.Uri.ToString().Contains("search-filter") || page.Uri.ToString().Contains("customer") || page.Uri.ToString().Contains("productVariantGroup"))
                return;
            ExportedCsvModel model = new ExportedCsvModel();
            model = AddName(page, model);
            if (string.IsNullOrEmpty(model.Name))
                return;
            model = AddPrice(page, model);
            model = FormatPrice(model);
            model = AddImage(page, model);
            model = AddCategory(page, model);
            CsvBuilder.TryAddRow(model);
        }

        private static ExportedCsvModel FormatPrice(ExportedCsvModel model)
        {
            model.Price = model.Price.Replace('z', ' ');
            model.Price = model.Price.Replace('ł', ' ');
            model.Price = model.Price.Trim();
            model.Price = Regex.Replace(model.Price, @"\s+", "");
            return model;
        }

        private static ExportedCsvModel AddPrice(CrawledPage crawledPage, ExportedCsvModel model)
        {
            var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-prices");
            var a = x?.ChildNodes.FindFirst("div");
            var b = a?.ChildNodes.FindFirst("span");
            var c = b?.ChildNodes.FindFirst("span");
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
            var price = c?.ChildNodes.FindFirst("span")?.InnerText;

            if (price != null)
            {
                if (price == "90")
                    price = GetPriceWithCents(c);
                model.Price = price;
            }

            return model;
        }
        private static ExportedCsvModel AddName(CrawledPage crawledPage, ExportedCsvModel model)
        {
            var name = crawledPage.HtmlDocument.GetElementbyId("p-inner-name");
            var nameFormatted = name?.ChildNodes.FindFirst("h1").InnerText;
            nameFormatted = nameFormatted?.Replace('\n', ' ');
            nameFormatted = nameFormatted?.Trim();
            model.Name = nameFormatted;

            return model;
        }

        private static ExportedCsvModel AddImage(CrawledPage crawledPage, ExportedCsvModel model)
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
                model.ImageUrl = c.Substring(pFrom, pTo - pFrom);
            }

            return model;
        }

        private static ExportedCsvModel AddCategory(CrawledPage crawledPage, ExportedCsvModel model)
        {
            var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-gallery");
            var a = x?.ChildNodes.FindFirst("ktr-gallery");
            var b = a?.ChildNodes.FindFirst("div");
            var c = b?.ChildNodes.FindFirst("div").InnerHtml;

            model.Category = "CATEGORY";

            return model;
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
