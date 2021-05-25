using System.Collections.Generic;
using System.Timers;
using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace JobScheduler
{
    public class JobScheduler
    {
        private readonly Timer _timer;
        private readonly List<IJob> _jobs = new();
        private CancellationTokenSource _tokenSource;
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
            OnTimedEventAsync(@event);
        }

        private async Task OnTimedEventAsync(ElapsedEventArgs @event)
        {
            var task1 = ExecuteSimpleJobs(@event);
            var task2 = ExecuteDelayedJobs(@event);

            await Task.WhenAll(task1, task2);
        }

        private async Task ExecuteSimpleJobs(ElapsedEventArgs @event)
        {
            await ExecuteJobs(_jobs, @event.SignalTime);
        }

        private async Task ExecuteDelayedJobs(ElapsedEventArgs @event)
        {
            await ExecuteJobs(_delayedJobs.Select(x => x as IJob), @event.SignalTime);
        }

        private async Task ExecuteJobs(IEnumerable<IJob> jobs, DateTime startAt)
        {
            foreach (var job in jobs)
            {
                if (await job.ShouldRun(startAt))
                {
                    await ExecuteJob(job, startAt);
                }
            }
        }

        private async Task ExecuteJob(IJob job, DateTime signalTime)
        {
            try
            {
                await job.Execute(signalTime, _tokenSource.Token);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                job.MarkAsFailed();
            }
        }
    }
}
