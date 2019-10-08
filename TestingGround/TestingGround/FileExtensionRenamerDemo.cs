using System;
using System.Collections.Generic;
using System.Text;
using FileManipMacros;

namespace TestingGroundCLI
{
    public static class FileExtensionRenamerDemo
    {
        public static void Start()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the FileExtensionRenamer Demo :)");
                Console.WriteLine("Which method would you like to use?");
                Console.WriteLine(" 1.) FileExtensionRename");
                Console.WriteLine(" 2.) RecursiveExtensionRename");
                Console.WriteLine("99.) Quit to Main Menu");
                try
                {
                    int selection = int.Parse(Console.ReadLine());
                    switch (selection)
                    {
                        case 1:
                            FileExtensionRenameDemo();
                            break;
                        case 2:
                            RecursiveExtensionRenameDemo();
                            break;
                        case 99:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid input.");
                            break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }

        public static void FileExtensionRenameDemo()
        {
            Console.WriteLine("Enter the fully qualified path to the file: ");
            string filePath = Console.ReadLine();
            Console.WriteLine("Enter the new file entension (without the \".\"): ");
            string newExtension = Console.ReadLine();
            FileManip.FileExtensionRename(filePath, newExtension);
        }

        public static void RecursiveExtensionRenameDemo()
        {
            Console.WriteLine("Enter the path to the base directory to begin renaming: ");
            string filePath = Console.ReadLine();
            Console.WriteLine("Enter the current file entension (without the \".\"): ");
            string currentExtension = Console.ReadLine(); 
            Console.WriteLine("Enter the new file entension (without the \".\"): ");
            string newExtension = Console.ReadLine();
            FileManip.RecursiveExtensionRename(filePath, currentExtension, newExtension);
        }
    }
}