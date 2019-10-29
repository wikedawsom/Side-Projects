using FramerateStabilizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace TestingGroundCLI
{
    public static class FramerateStabilizerDemo
    {
        public static void Start()
        {
            Console.WriteLine("Hello World!");
            Console.Write("Enter number of frames: ");
            int numFrames = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter milliseconds per frame: ");
            int ms = Convert.ToInt32(Console.ReadLine());
            Stopwatch timer = new Stopwatch();
            timer.Start();

            for (int i = 0; i < numFrames; i++)
            {
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                StableFrames.Stabilize(ms, timer);
                Console.WriteLine(timer.ElapsedMilliseconds + " milliseconds elapsed for " + (i + 1) + " frames.");
            }
            
            timer.Stop();
            Console.ReadKey();
        }
    }
}
