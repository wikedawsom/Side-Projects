using System;

namespace TestingGroundCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            TestingGroundMenu.Menu();

            Console.WriteLine("Program finished. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
