using System.Collections.Generic;
using System.Timers;

namespace JobScheduler
{
    public class JobScheduler 
    {
        private readonly Timer _timer;
        private readonly List<IJob> _jobs = new List<IJob>();

        public JobScheduler(int intervalMs)
        {
            _timer = new Timer(intervalMs);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        public void AddHandler(IJob action)
        {
            _jobs.Add(action);
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            foreach (var job in _jobs)
            {
                job.Execute(@event.SignalTime);
            }
        }
    }
}
