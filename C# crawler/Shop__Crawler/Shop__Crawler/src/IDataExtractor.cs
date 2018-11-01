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
        ExportedCsvModel AddDescription(CrawledPage crawledPage, ExportedCsvModel model);
        ExportedCsvModel AddCategories(CrawledPage crawledPage, ExportedCsvModel model);
        ExportedCsvModel FormatPrice(ExportedCsvModel model);
        bool IsNotProductPage(CrawledPage page);
    }
}