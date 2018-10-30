using Abot.Core;
using Abot.Crawler;
using Abot.Poco;
using log4net.Config;
using System;
using System.Net;

namespace Shop__Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            new Crawler().Start();
            Console.ReadKey();
        }
    }

    public class Crawler
    {
        private readonly FileSystem _fileSystem = new FileSystem();

        public void Start()
        {
            PoliteWebCrawler crawler = SetUp();

            CrawlResult result =
                crawler.Crawl(
                    new Uri("https://www.komputronik.pl/product/555987/philips-stylecare-prestige-bhb878-00.html"));

            if (result.ErrorOccurred)
            {
                Console.WriteLine("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri,
                    result.ErrorException.Message);
            }
            else
            {
                Console.WriteLine("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);
                _fileSystem.Save();
                Console.WriteLine("Saved successfully");
            }

        }

        private PoliteWebCrawler SetUp()
        {
            XmlConfigurator.Configure();

            CrawlConfiguration crawlConfig = AbotConfigurationSectionHandler.LoadFromXml().Convert();
            crawlConfig.MaxConcurrentThreads = 5;//this overrides the config value

            // Ostrożnie z głębokością, 0 i tak sporo zwraca rekordów
            crawlConfig.MaxCrawlDepth = 0;

            //Will use app.config for configuration
            PoliteWebCrawler crawler = new PoliteWebCrawler();
            crawler.ShouldCrawlPage(ShouldCrawlPage);

            crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
            crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
            crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;
            return crawler;
        }

        private CrawlDecision ShouldCrawlPage(PageToCrawl page, CrawlContext context)
        {
            var result = new CrawlDecision();
            if (page.Uri.ToString().Contains("category") || page.Uri.ToString().Contains("informacje"))
            {
                result.Reason = "CATEGORY / INFORMACJE";
                result.Allow = false;
            }
            else
            {
                result.Allow = true;
            }

            return result;
        }

        //after page is crawled, here's the result
        void crawler_ProcessPageCrawlCompleted(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;

            if (crawledPage.WebException != null || crawledPage.HttpWebResponse.StatusCode != HttpStatusCode.OK)
                Console.WriteLine("Crawl of page failed {0}", crawledPage.Uri.AbsoluteUri);
            else
                Console.WriteLine("Crawl of page succeeded {0}", crawledPage.Uri.AbsoluteUri);

            if (string.IsNullOrEmpty(crawledPage.Content.Text))
                Console.WriteLine("Page had no content {0}", crawledPage.Uri.AbsoluteUri);

            var result = DataFinder.FindNameAndPrice(crawledPage);
            if(result != "")
            {
                _fileSystem.AddLine(result);
            }
        }

        void crawler_PageLinksCrawlDisallowed(object sender, PageLinksCrawlDisallowedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            Console.WriteLine("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
        }

        void crawler_PageCrawlDisallowed(object sender, PageCrawlDisallowedArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
        }
    }
}
