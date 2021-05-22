using System;
using System.Threading.Tasks;
using JobScheduler;

namespace AnalyticsProgram.Jobs
{
    public abstract class BaseJob : IJob
    {
        private bool _isFailed;

        public abstract Task Execute(DateTime signalTime);

        public virtual Task<bool> ShouldRun(DateTime signalTime)
        {
            return Task.FromResult(!_isFailed);
        }

        public virtual void MarkAsFailed()
        {
            _isFailed = true;
        }
    }
}
