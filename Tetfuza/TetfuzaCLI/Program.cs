using System;

namespace TetfuzaCLI
{
    public class Program
    {

        static void Main(string[] args)
        {
            MainScreen CLIWindow = new MainScreen();
            CLIWindow.StartCLI();

            Console.WriteLine("Program Finnish...");
        }

    }
}
