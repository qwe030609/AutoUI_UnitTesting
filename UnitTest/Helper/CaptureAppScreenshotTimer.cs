using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PP5AutoUITests
{
    public class CaptureAppScreenshotTimer
    {
        private Timer _timer;
        private Action<string, string> _action;
        private string _param1;
        private string _param2;
        private bool _running;

        public CaptureAppScreenshotTimer(Action<string, string> action, string param1, string param2, double updateMilliseconds)
        {
            _action = action;
            _param1 = param1;
            _param2 = param2;

            _timer = new Timer(updateMilliseconds);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
        }

        public void Start()
        {
            //_timer.Enabled = true;
            _running = true;
            Task.Run(() => RunTimer());
        }

        public void Stop()
        {
            //_timer.Enabled = false;
            _running = false;
            _timer.Stop();
        }

        private void RunTimer()
        {
            _timer.Start();
            while (_running)
            {
                Task.Delay(10).Wait(); // Wait to reduce CPU usage
            }
        }


        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}", e.SignalTime);
            //_action(_param1, _param2);
            //var task = new Task(_action(_param1, _param2));
            //bool res = await task.Start();
            lock (_timer)
            {
                Task.Run(() => _action(_param1, _param2));
            }
        }
    }
}
