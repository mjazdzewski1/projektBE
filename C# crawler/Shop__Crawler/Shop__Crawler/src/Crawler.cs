using System;
using System.Net;
using Abot.Core;
using Abot.Crawler;
using Abot.Poco;
using log4net.Config;
using Shop__Crawler.Models;
using Shop__Crawler.src;

namespace Shop__Crawler
{
    public class Crawler
    { 
        private readonly FileSystem _fileSystem = new FileSystem();

        public void Start()
        {
            PoliteWebCrawler crawler = new CrawlerConfig().CreateCrawler();

            CrawlResult result =
                crawler.Crawl(
                    //new Uri("https://www.komputronik.pl/category/17631/lenovo-ideapad.html"));
                    //new Uri("https://www.komputronik.pl/category/17623/laptopy-lenovo.html"));
                    //new Uri("https://www.komputronik.pl/category/5022/laptopy.html")); // <- ten jest spoko
                    new Uri("https://www.komputronik.pl/category/5801/komputery-pc.html"));


            if (result.ErrorOccurred)
            {
                Console.WriteLine("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri,
                    result.ErrorException.Message);
            }
            else
            {
                Console.WriteLine("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);
                Console.WriteLine("Saved successfully");
            }
        }
    }
}