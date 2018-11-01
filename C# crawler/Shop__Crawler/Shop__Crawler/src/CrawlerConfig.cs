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
using Shop__Crawler.Models;

namespace Shop__Crawler.src
{
    class CrawlerConfig
    {
        public PoliteWebCrawler CreateCrawler()
        {
            XmlConfigurator.Configure();

            CrawlConfiguration crawlConfig = AbotConfigurationSectionHandler.LoadFromXml().Convert();
            crawlConfig.MaxConcurrentThreads = 15;//this overrides the config value

            crawlConfig.MaxCrawlDepth = 8;

            //Will use app.config for configuration
            PoliteWebCrawler crawler = new PoliteWebCrawler();
            crawler.ShouldCrawlPage(ShouldCrawlPage);
            crawler.ShouldDownloadPageContent(ShouldCrawlPageContent);
            crawler.ShouldCrawlPageLinks(ShouldCrawlPageLinks);

            crawler.PageCrawlCompletedAsync += crawler_ProcessPageCrawlCompleted;
            crawler.PageCrawlDisallowedAsync += crawler_PageCrawlDisallowed;
            crawler.PageLinksCrawlDisallowedAsync += crawler_PageLinksCrawlDisallowed;
            return crawler;
        }

        private CrawlDecision ShouldCrawlPageLinks(CrawledPage page, CrawlContext context)
        {
            var result = new CrawlDecision();
            if (page.Uri.ToString().Contains("product") || page.Uri.ToString().Contains("category") ||
                page.Uri.ToString().Contains("productVariantGroup") || page.Uri.ToString().Contains("p=")|| 
                page.Uri.ToString().Contains("lenovo") || 
                page.Uri.ToString().Contains("laptop")) 
            {
                result.Allow = true;
            }
            else
            {
                result.Reason = "No no nO";
                result.Allow = false;
            }

            return result;
        }

        private CrawlDecision ShouldCrawlPageContent(CrawledPage page, CrawlContext context)
        {
            var result = new CrawlDecision();
            if (page.Uri.ToString().Contains("product") ||
                page.Uri.ToString().Contains("lenovo") ||
                page.Uri.ToString().Contains("laptop")) 
            {
                result.Allow = true;
            }
            else
            {
                result.Reason = "Not a product";
                result.Allow = false;
            }

            return result;
        }

        private CrawlDecision ShouldCrawlPage(PageToCrawl page, CrawlContext context)
        {
            var result = new CrawlDecision();
            //if (page.Uri.ToString().Contains("category") || page.Uri.ToString().Contains("informacje"))
            //{
            //    result.Reason = "CATEGORY / INFORMACJE";
            //    result.Allow = false;
            //}
            //else
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

            DataFinder.Run(crawledPage);
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
