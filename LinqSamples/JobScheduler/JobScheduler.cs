using System;
using System.Collections.Generic;
using System.Timers;

namespace JobScheduler
{
    interface IJob
    {
        void Execute(DateTime signalTime);
    }

    public class JobScheduler
    {
        private readonly Timer _timer;
        private readonly List<Action<DateTime>> _actions = new();

        public JobScheduler(int intervalMs)
        {
            _timer = new Timer(intervalMs);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        public void AddHandler(Action<DateTime> action)
        {
            _actions.Add(action);
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            foreach (var action in _actions)
            {
                action?.Invoke(@event.SignalTime);
            }
        }
    }
}
