using System;
using System.Threading;

namespace Notebook.SignalRApi.TimersFeatures
{
    public class TimerManager
    {
        private readonly Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private readonly Action _action;
        private DateTime TimerStarted { get; set; }

        public TimerManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, action, 1000, 2000);
            TimerStarted = DateTime.Now;
        }

        private void Execute(object? state)
        {
            _action();

            if ((DateTime.Now - TimerStarted).Seconds > 60) 
                _timer.Dispose();
        }
    }
}
