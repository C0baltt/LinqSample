using System;
using System.Threading.Tasks;

namespace AnalyticsProgram.Jobs
{
    public class WriteToConsole : BaseJob
    {
    public DateTime StartJob { get; set; }

        public WriteToConsole() : this(DateTime.MinValue)
        {

        }

        public WriteToConsole(DateTime timeStart)
        {
            StartJob = timeStart;
        }

        public override Task Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
            return Task.CompletedTask;
        }
    }
}
