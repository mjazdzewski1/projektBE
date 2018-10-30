using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Abot.Core;
using Abot.Crawler;
using Abot.Poco;
using log4net.Config;

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
        private FileSystem _fileSystem = new FileSystem();

        public void Start()
        {
            XmlConfigurator.Configure();


            CrawlConfiguration crawlConfig = AbotConfigurationSectionHandler.LoadFromXml().Convert();
            crawlConfig.MaxConcurrentThreads = 5;//this overrides the config value
            crawlConfig.MaxCrawlDepth = 0;

            //Will use app.config for configuration
            PoliteWebCrawler crawler = new PoliteWebCrawler();
            crawler.ShouldCrawlPage(ShouldCrawl);

            crawler.PageCrawlStartingAsync += crawler_ProcessPageCrawlStarting;
            crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
            crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
            crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;

            CrawlResult result = crawler.Crawl(new Uri("https://www.komputronik.pl/product/555987/philips-stylecare-prestige-bhb878-00.html")); //This is synchronous, it will not go to the next line until the crawl has completed

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

        private CrawlDecision ShouldCrawl(PageToCrawl page, CrawlContext context)
        {
            var result = new CrawlDecision();
            if (page.Uri.ToString().Contains("category"))
            {
                result.Reason = "CATEGORY";
                result.Allow = false;
            }
            else
            {
                result.Allow = true;
            }

            return result;

        }

        //single page crawl started
        void crawler_ProcessPageCrawlStarting(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            //Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
            //_fileSystem.AddLink(pageToCrawl.Uri.AbsoluteUri);
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

            var htmlAgilityPackDocument = crawledPage.HtmlDocument; //Html Agility Pack parser
            var angleSharpHtmlDocument = crawledPage.AngleSharpHtmlDocument; //AngleSharp parser

            //var x = crawledPage.HtmlDocument.GetElementbyId("p-inner-prices");
            //var a = x?.ChildNodes.FindFirst("div");
            //var b = a?.ChildNodes.FindFirst("span");
            //var c = b?.ChildNodes.FindFirst("span");
            //if (c != null)
            //{
            //    var price = c.ChildNodes.FindFirst("span")?.InnerText;
            //    Console.WriteLine(price ?? "NULL");
            //    if (price != null)
            //    {
            //        var name = crawledPage.HtmlDocument.GetElementbyId("p-inner-name");
            //        var name2 = name?.ChildNodes.FindFirst("h1").InnerText;
            //        name2 = name2?.Replace('\n', ' ');
            //        name2 = name2?.Trim();
            //        if(name2 == null)
            //            Console.WriteLine();
            //        _fileSystem.AddLink($"{name2}: {price}");
            //    }
            //}
            _fileSystem.AddLink(DataFinder.FindNameAndPrice(crawledPage));
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
