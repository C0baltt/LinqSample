using System;

namespace JobScheduler
{
   public interface IJob
    {
        public bool ShouldStart { get; set; }
        DateTime StartJob { get; set; }

        void Execute(DateTime signalTime);
    }
}
