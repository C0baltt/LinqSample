using System;
using System.Globalization;
using System.Threading.Tasks;

namespace AnalyticsProgram.Jobs

{
   public class ExecutionTimeFileLoggerJob : BaseJob
    {
        private const string Path = "ExecutionTimeLog.txt";
        private bool _isFailed;

        public override Task<bool> ShouldRun(DateTime signalTime)
        {
            return Task.FromResult(!_isFailed);
        }

        public DateTime StartJob { get; set; }

        public ExecutionTimeFileLoggerJob() : this(DateTime.MinValue)
        {

        }

        public ExecutionTimeFileLoggerJob(DateTime timeStart)
        {
            StartJob = timeStart;
        }

        public override Task Execute(DateTime signalTime)
        {
            FileUtils.WriteToFile(Path,
                signalTime.ToString(CultureInfo.InvariantCulture));
            return Task.CompletedTask;
        }

        public override void MarkAsFailed()
        {
            _isFailed = true;
        }
    }
}
