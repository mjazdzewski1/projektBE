using CsvHelper;
using System;
using System.IO;

namespace CsvBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var allLines = File.ReadAllLines("crawled.txt");

            Random rand = new Random();

            for (int i = 0; i < 700; i++)
            {
                var lineNum = rand.Next(allLines.Length);
                System.IO.File.AppendAllText(@"products.csv", allLines[lineNum] + "\n");
            }

            Console.WriteLine("DONE");

            Console.ReadKey();
        }
    }
}
