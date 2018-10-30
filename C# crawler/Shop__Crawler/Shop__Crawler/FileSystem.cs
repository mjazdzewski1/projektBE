using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop__Crawler
{
    class FileSystem
    {
        private string _links = "";

        public void AddLink(string crawledLink)
        {
            _links += "\n" +crawledLink;
        }

        public void Save()
        {
            System.IO.File.WriteAllText(@"crawledLinks.txt", _links);
        }
    }
}
