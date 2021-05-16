using System;
using JobScheduler;
using System.Globalization;

namespace AnalyticsProgram
{
   public class ExecutionTimeFileLoggerJob : IJob
    {
        private const string Path = "ExecutionTimeLog.txt";

        public bool IsFailed { get; set; }

        public DateTime StartJob { get; set; }

        public ExecutionTimeFileLoggerJob() : this(DateTime.MinValue)
        {

        }

        public ExecutionTimeFileLoggerJob(DateTime timeStart)
        {
            StartJob = timeStart;
        }

        public void Execute(DateTime signalTime)
        {
            WriteFile.WriteToFile(Path,
                signalTime.ToString(CultureInfo.InvariantCulture));
        }
    }
}
