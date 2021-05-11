using System;
using JobScheduler;
using System.Globalization;
using System.Text;
using System.IO;

namespace AnalyticsProgram
{
   public class ExecutionTimeFileLoggerJob : IJob
    {
        public void Execute(DateTime signalTime)
        {
            Program.WriteToFile("ExecutionTimeLog.txt", signalTime.ToString(CultureInfo.InvariantCulture));
        }
    }
}
