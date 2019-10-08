using System;
using System.Diagnostics;

namespace FramerateStabilizer
{
    class Program
    {
        static void Main(string[] args)
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
