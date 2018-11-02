using Abot.Poco;
using Shop__Crawler.Models;

namespace Shop__Crawler
{
    class DataFinder
    {
        private readonly IDataExtractor _dataExtractor;

        public DataFinder(IDataExtractor dataExtractor)
        {
            _dataExtractor = dataExtractor;
        }
        public void Run(CrawledPage page)
        {
            if (_dataExtractor.IsNotProductPage(page))
                return;
            ExportedCsvModel model = new ExportedCsvModel();
            model = _dataExtractor.AddName(page, model);
            if (string.IsNullOrEmpty(model.Name))
                return;

            model = _dataExtractor.AddPrice(page, model);
            model = _dataExtractor.FormatPrice(model);
            model = _dataExtractor.AddImage(page, model);
            model = _dataExtractor.AddCategory(page, model);
            model = _dataExtractor.AddDescription(page, model);
            model = _dataExtractor.AddCategories(page, model);

            CsvBuilder.TryAddRow(model);
        }
    }
}
