using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobScheduler
{
   public interface IJob
    {
        Task Execute(DateTime signalTime, CancellationToken token);

        Task<bool> ShouldRun(DateTime signalTime);

        void MarkAsFailed();
    }
}
