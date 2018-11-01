using Abot.Poco;
using HtmlAgilityPack;
using Shop__Crawler.Models;

namespace Shop__Crawler
{
    public interface IDataExtractor
    {
        ExportedCsvModel AddCategory(CrawledPage crawledPage, ExportedCsvModel model);
        ExportedCsvModel AddImage(CrawledPage crawledPage, ExportedCsvModel model);
        ExportedCsvModel AddName(CrawledPage crawledPage, ExportedCsvModel model);
        ExportedCsvModel AddPrice(CrawledPage crawledPage, ExportedCsvModel model);
        ExportedCsvModel FormatPrice(ExportedCsvModel model);
        string GetPriceWithCents(HtmlNode parentNode);
        bool IsNotProductPage(CrawledPage page);
    }
}