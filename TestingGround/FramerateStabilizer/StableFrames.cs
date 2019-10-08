using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace FramerateStabilizer
{
    public static class StableFrames
    {
        
        public static void Stabilize(int iterationsPerSecond, Stopwatch timer)
        {
            double secondsPerIteraion = 1.0 / iterationsPerSecond;
            double milisecondsPerIteration = secondsPerIteraion * 1000;
            double sleepTime = milisecondsPerIteration - (timer.ElapsedMilliseconds % milisecondsPerIteration);
            Thread.Sleep((int)sleepTime);
        }
    }

    public class BetterStableFrames
    {
        private static long _currentFrame;
        public BetterStableFrames()
        {
            _currentFrame = 0;
        }
        public void Stabilize(int iterationsPerSecond, Stopwatch timer)
        {
            if (iterationsPerSecond == 0)
            {
                return;
            }
            double secondsPerIteraion = 1.0 / iterationsPerSecond;
            double milisecondsPerIteration = secondsPerIteraion * 1000;
            double sleepTime = 0;
            long currentFrame = timer.ElapsedMilliseconds / (long)milisecondsPerIteration;
            if (currentFrame == _currentFrame)
            {
                sleepTime = milisecondsPerIteration - (timer.ElapsedMilliseconds % milisecondsPerIteration);
                _currentFrame++;
            }
            else
            {
                _currentFrame = currentFrame;
            }
            Thread.Sleep((int)sleepTime);
        }
    }
}
