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
            char userInputED = Console.ReadKey().KeyChar;
            Console.WriteLine();
            Console.Write("Small or large file? (s/l): ");
            char userInputSL = Console.ReadKey().KeyChar;
            Console.WriteLine();
            Console.Write("Specify offset count: ");

            int offset = 1;
            int.TryParse(Console.ReadLine(), out offset);

            Console.WriteLine("Working...");
            try
            {
                if (userInputED == 'e' && userInputSL == 's')
                {
                    FileEncoder.EncryptFileStandard(path, (byte)offset);
                }
                else if (userInputED == 'd' && userInputSL == 's')
                {
                    FileEncoder.DecryptFileStandard(path, (byte)offset);
                }
                else if (userInputED == 'e' && userInputSL == 'l')
                {
                    FileEncoder.EncryptFileStream(path, (byte)offset);
                }
                else if (userInputED == 'd' && userInputSL == 'l')
                {
                    FileEncoder.DecryptFileStream(path, (byte)offset);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File read/write failed. Please ensure you have a correct path and appropriate permissions.");
                Console.ReadKey();
            }
            Console.WriteLine("Program Finished");
            Console.ReadKey();
        }
    }
}
