using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using Shop__Crawler.Models;

namespace Shop__Crawler
{
    public static class CsvBuilder
    {
        private static readonly FileSystem _fileSystem = new FileSystem();
        private static readonly StringWriter _stringWriter = new StringWriter();
        private static readonly CsvWriter _csvWriter = new CsvWriter(_stringWriter);

        public static void TryAddRow(ExportedCsvModel row)
        {
            if (row.IsValid())
            {
                _csvWriter.WriteRecord(row);
                _csvWriter.NextRecord();
            }
            else
            {
                Console.WriteLine("INVALID ROW");
            }
        }

        public static void AddRows(List<ExportedCsvModel> rows)
        {
            _csvWriter.WriteRecords(rows);
        }

        public static void Save()
        {
            _csvWriter.Flush();
            _fileSystem.AddString(_stringWriter.ToString());
            _fileSystem.Save();
        }
    }
}
