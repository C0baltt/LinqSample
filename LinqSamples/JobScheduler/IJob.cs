using System;
using System.Threading.Tasks;

namespace JobScheduler
{
    public interface IJob
    {
        Task Execute(DateTime signalTime);
        Task<bool> ShouldRun(DateTime signalTime);
        void MarkAsFailed();
    }
}
