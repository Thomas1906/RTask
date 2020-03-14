using System.IO;
using NUnit.Framework;
using RTask.Services;

namespace RTask.Tests
{
    [TestFixture]
    public class FileReaderServiceTests
    {
        private IFileReaderService _fileReaderService;
        private readonly string _inputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        private readonly string _outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "output.txt");
        private const string InputContent = 
            "# Material inventory initial state as of Jan 01 2018\r\n" +
            "# New materials\r\n" +
            "Cherry Hardwood Arched Door - PS;COM-100001;WH-A,5|WH-B,10\r\n" +
            "Maple Dovetail Drawerbox;COM-124047;WH-A,15\r\n" +
            "Generic Wire Pull;COM-123906c;WH-A,10|WH-B,6|WH-C,2\r\n" +
            "Yankee Hardware 110 Deg. Hinge;COM-123908;WH-A,10|WH-B,11\r\n" +
            "# Existing materials, restocked\r\n" +
            "Hdw Accuride CB0115-CASSRC - Locking Handle Kit - Black;CB0115-CASSRC;WH-C,13|WH-B,5\r\n" +
            "Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back;3M-Cherry-10mm;WH-A,10|WH-B,1\r\n" +
            "Veneer - Cherry Rotary 1 FSC;COM-123823;WH-C,10\r\n" +
            "MDF, CARB2, 1 1/8\";COM-101734;WH-C,8\r\n";
        private const string OutputContent =
            "WH-A (total 50)\r\n3M-Cherry-10mm: 10\r\nCOM-100001: 5\r\nCOM-123906c: 10\r\nCOM-123908: 10\r\nCOM-124047: 15\r\n\r\n"+
            "WH-C (total 33)\r\nCB0115-CASSRC: 13\r\nCOM-101734: 8\r\nCOM-123823: 10\r\nCOM-123906c: 2\r\n\r\n"+
            "WH-B (total 33)\r\n3M-Cherry-10mm: 1\r\nCB0115-CASSRC: 5\r\nCOM-100001: 10\r\nCOM-123906c: 6\r\nCOM-123908: 11\r\n\r\n";


        [SetUp]
        public void Setup()
        {
            _fileReaderService = new FileReaderService();
        }

        [Test]
        public void FileExists_NonExist_ShouldReturnFalse_Test()
        {
            DeleteFile(_inputFilePath);
            Assert.IsFalse(_fileReaderService.FileExists());
        }

        [Test]
        public void FileExists_Exist_ShouldReturnTrue_Test()
        {
            DeleteFile(_inputFilePath);
            CreateFile(_inputFilePath);
            Assert.IsTrue(_fileReaderService.FileExists());
        }

        [Test]
        public void ReadFile_EmptyFile_ShouldReturnEmpty_Test()
        {
            DeleteFile(_inputFilePath);
            CreateFile(_inputFilePath, "");
            var text = _fileReaderService.ReadFile();
            Assert.IsEmpty(text);
        }

        [Test]
        public void ReadFile_CorrectFile_ShouldReturnText_Test()
        {
            DeleteFile(_inputFilePath);
            CreateFile(_inputFilePath, InputContent);
            var text = _fileReaderService.ReadFile();
            Assert.IsNotEmpty(text);
        }

        [Test]
        public void SaveFile_FileNonExists_ShouldCreateFile_Test()
        {
            DeleteFile(_outputFilePath);
            _fileReaderService.SaveFile(OutputContent);
            Assert.IsTrue(FileExists(_outputFilePath));
        }

        [Test]
        public void SaveFile_FileExists_ShouldDeleteOldFileAndCreateNewFile_Test()
        {
            DeleteFile(_outputFilePath);
            CreateFile(_outputFilePath);
            _fileReaderService.SaveFile(OutputContent);
            Assert.IsTrue(FileExists(_outputFilePath));
        }

        private static void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }

        private static void CreateFile(string filePath)
        {
            File.Create(filePath).Dispose();
        }

        private static void CreateFile(string filePath, string content)
        {
            using var tw = new StreamWriter(filePath, false);
            tw.WriteLine(content);
        }

        private static bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
