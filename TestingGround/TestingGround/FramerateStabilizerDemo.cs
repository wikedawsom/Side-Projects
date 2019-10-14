using FramerateStabilizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace TestingGroundCLI
{
    class FramerateStabilizerDemo
    {
        static void Menu()
        {
            Console.WriteLine("Hello World!");
            BetterStableFrames consistentFramerateTracker = new BetterStableFrames();
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < 5; i++)
            {
                StableFrames.Stabilize(1, timer);
                //consistentFramerateTracker.Stabilize(60, timer);
            }
            Console.WriteLine(timer.ElapsedMilliseconds);
            timer.Stop();
            Console.ReadKey();
        }
    }
}
