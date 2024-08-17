using System;
using System.Diagnostics;
using System.Threading;

namespace PP5AutoUITests
{
    internal class StopwatchWrapper
    {
        private readonly Stopwatch stopwatch;
        private bool isStart;

        internal StopwatchWrapper() 
        {
            // Create a Stopwatch
            stopwatch = new Stopwatch();
        }

        internal void Start() 
        {
            stopwatch.Start();
            isStart = true;
        }

        internal void Stop() 
        {
            if(isStart)
            {
                stopwatch.Stop();
                isStart = false;
            }
        }

        internal void Reset() 
        {
            stopwatch.Reset();
        }

        internal long GetElapsedTimeInMilliSeconds() 
        {
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
