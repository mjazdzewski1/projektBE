using System;
using System.Collections.Generic;
using Shop__Crawler.Models;

namespace Shop__Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvBuilder.SetUp();
            new Crawler().Start();

            Console.WriteLine("DONE!");
            Console.ReadKey();
        }
    }
}
