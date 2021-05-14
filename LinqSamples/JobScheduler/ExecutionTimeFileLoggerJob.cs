using System;
using JobScheduler;
using System.Globalization;

namespace AnalyticsProgram
{
   public class ExecutionTimeFileLoggerJob : IJob
    {
        public void Execute(DateTime signalTime)
        {
            WriteFile.WriteToFile("ExecutionTimeLog.txt",
                signalTime.ToString(CultureInfo.InvariantCulture));
        }
    }
}
