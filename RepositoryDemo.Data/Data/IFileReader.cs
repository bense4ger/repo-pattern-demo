namespace RepositoryDemo.Data.Data
{
    public interface IFileReader
    {
        void Configure(string filename);
        string ReadText();
        string[] ReadLines();
    }
}