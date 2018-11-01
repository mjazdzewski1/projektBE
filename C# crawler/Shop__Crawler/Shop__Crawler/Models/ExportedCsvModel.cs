using System;

namespace Shop__Crawler.Models
{
    public class ExportedCsvModel
    {
        public string Name { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public bool IsValid() => Name != String.Empty && Price != string.Empty && ImageUrl != string.Empty && Category != string.Empty;
    }
}
