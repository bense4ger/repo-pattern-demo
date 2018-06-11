using System.IO;

namespace RepositoryDemo.Data.Data
{
    public class FileReader : IFileReader
    {
        private readonly string _filepath;
        private string _filename;
        
        public FileReader(string filepath)
        {
            _filepath = filepath;
        }

        private string DataSource => Path.Combine(_filepath, _filename);
        
        public void Configure(string filename)
        {
            _filename = filename;
        }

        public string ReadText() => !File.Exists(DataSource) ? string.Empty : File.ReadAllText(DataSource);
        public string[] ReadLines() => !File.Exists(DataSource) ? null : File.ReadAllLines(DataSource);
    }
}