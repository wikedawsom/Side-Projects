using System;
using System.Collections.Generic;
using System.Text;

namespace TestingGroundCLI
{
    public static class TestingGroundMenu
    {
        public static void Menu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to my testing ground of random helpful methods.");
                Console.WriteLine("Which Project do you want to demo?");
                Console.WriteLine(" 1.) Framerate Stabilizer");
                Console.WriteLine(" 2.) File Extension Changer");
                Console.WriteLine("99.) Quit");
                Console.WriteLine("Selection #: ");
                try
                {
                    int selection = int.Parse(Console.ReadLine());
                    switch (selection)
                    {
                        case 1:
                            FramerateStabilizerDemo.Start();
                            break;
                        case 2:
                            FileExtensionRenamerDemo.Start();
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
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}
