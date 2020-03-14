namespace RTask.Services
{
    public interface IFileReaderService
    {
        bool FileExists();
        string[] ReadFile();
        void SaveFile(string outputString);
    }
}
