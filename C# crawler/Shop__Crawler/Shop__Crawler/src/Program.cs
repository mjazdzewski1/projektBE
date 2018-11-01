using System;
using System.Collections.Generic;
using Shop__Crawler.Models;

namespace Shop__Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            new Crawler().Start();

            CsvBuilder.Save();
            Console.WriteLine("DONE!");
            Console.ReadKey();
        }
    }
}
