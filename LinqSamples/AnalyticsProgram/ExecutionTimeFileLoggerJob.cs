using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsProgram
{
   public class ExecutionTimeFileLoggerJob : IJob
    {

        public void Execution(DateTime signalTime)
        {
            Program.WriteToFile("ExecutionTimeLog.txt", signalTime.ToString(CultureInfo.InvariantCulture));
        }
    }
}
