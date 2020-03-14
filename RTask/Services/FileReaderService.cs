using System;
using System.IO;

namespace RTask.Services
{
    public class FileReaderService : IFileReaderService
    {
        private const string InputFileName = "input.txt";
        private const string OutputFileName = "output.txt";
        private readonly string _inputFilePath = Path.Combine(Directory.GetCurrentDirectory(), InputFileName);
        private readonly string _outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), OutputFileName);

        public bool FileExists()
        {
            if (File.Exists(_inputFilePath))
            {
                return true;
            }

            Console.WriteLine($"Could not find file {_inputFilePath}.");
            return false;
        }

        public string[] ReadFile()
        {
            var inputText = File.ReadAllText(_inputFilePath);
            return inputText.Split(new[] { Environment.NewLine, "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void SaveFile(string outputString)
        {
            File.WriteAllText(_outputFilePath, outputString);
        }
    }
}
