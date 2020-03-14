using System;
using RTask.Services;

namespace RTask
{
    static class Program
    {
        private static void Main()
        {
            IFileReaderService fileReaderService = new FileReaderService();
            if (fileReaderService.FileExists())
            {
                var inputLines = fileReaderService.ReadFile();
                if (inputLines.Length > 0)
                {
                    IInputMaterialsService inputMaterialsService = new InputMaterialsService();
                    var materialList = inputMaterialsService.CreateMaterialList(inputLines);
                    var outputString = inputMaterialsService.CreateOutputList(materialList);

                    fileReaderService.SaveFile(outputString);

                    Console.WriteLine(outputString);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press ANY key to exit");
            Console.ReadLine();
        }
    }
}
