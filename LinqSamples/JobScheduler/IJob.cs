using System;

namespace JobScheduler
{
   public interface IJob
    {
        void Execute(DateTime signalTime);
    }
}
