using System;

namespace JobScheduler
{
   public interface IJob
    {
        bool IsFailed { get; set; }

        DateTime StartJob { get; set; }

        void Execute(DateTime signalTime);
    }
}
