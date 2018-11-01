namespace Shop__Crawler
{
    class FileSystem
    {
        private string _fileContent = "";

        public void AddLine(string line)
        {
            _fileContent += "\n" +line;
        }

        public void AddString(string toAdd)
        {
            _fileContent += toAdd;
        }

        public void Save()
        {
            System.IO.File.WriteAllText(@"crawled.txt", _fileContent);
        }
    }
}
