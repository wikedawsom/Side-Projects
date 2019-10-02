/*  Author:             Christian Harris
 *  Date of last edit:  Oct. 2, 2019
 *  Description:        Main entry point for NumberGuesser in a console window
 */
using System;

namespace NumberGuesserConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new NumberGuesserConsoleClient();

            game.Start();
            
            Console.WriteLine("\n***Program end, press any key to exit***");
            Console.ReadKey();
        }
    }
}
