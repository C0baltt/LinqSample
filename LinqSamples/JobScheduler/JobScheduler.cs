using System.Collections.Generic;
using System.Timers;
using System;
using System.Linq;

namespace JobScheduler
{
    public class JobScheduler
    {
        private readonly Timer _timer;
        private readonly List<IJob> _jobs = new();
        private readonly List<IDelayedJob> _delayedJobs = new();

        public JobScheduler(int intervalMs)
        {
            _timer = new Timer(intervalMs);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        public void RegisterJob(IJob job)
        {
            _jobs.Add(job);
        }

        public void RegisterJob(IDelayedJob job)
        {
            _delayedJobs.Add(job);
        }

        public void Start()
        {
            if (_jobs.Count == 0)
            {
                throw new ArgumentException("Not added jobs!");
            }

            _timer.Start();
        }

        public void Stop() => _timer.Stop();

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            foreach (var job in _jobs.Where(j => j.ShouldStart && j.StartJob <= DateTime.Now))
            {
                try
                {
                    job.Execute(@event.SignalTime);

                    if (job.StartJob == DateTime.MinValue)
                    {
                        job.ShouldStart = false;
                    }
                }
                catch
                {
                    Console.WriteLine($"An error has occurred in class {job.GetType().Name}" +
                        $". DateTime: {DateTime.Now}");
                    job.ShouldStart = false;
                }
            }
        }
    }
}
