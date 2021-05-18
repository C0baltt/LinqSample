using System;

namespace JobScheduler
{
    public class WriteToConsole : IJob
    {
        public bool IsFailed { get; set; }

        public DateTime StartJob { get; set; }

        public WriteToConsole() : this(DateTime.MinValue)
        {

        }

        public WriteToConsole(DateTime timeStart)
        {
            StartJob = timeStart;
        }

        public void Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
        }
    }
}
