using System;
using TextFileEncoder;

namespace TextFileEncoderCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory;

            Console.WriteLine($"Current path is: {path}");
            Console.WriteLine("Enter relative file path (with preceeding \\): ");
            path += Console.ReadLine();

            Console.Write("Encode or decode? (e/d): ");
            char userInput = Console.ReadKey().KeyChar;
            if (userInput == 'e')
            {
                FileEncoder.EncodeFileStandard(path);
            }
            else if (userInput == 'd')
            {
                FileEncoder.DecodeFileStandard(path);
            }

            Console.ReadKey();
        }
    }
}
