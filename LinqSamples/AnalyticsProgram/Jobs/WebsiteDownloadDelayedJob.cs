using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Utils;

namespace AnalyticsProgram.Jobs
{
    public class WebsiteDownloadDelayedJob : BaseDelayedJob
    {
        public WebsiteDownloadDelayedJob(DateTime startAt) : base(startAt)
        {
        }

        public override async Task Execute(DateTime signalTime)
        {
            Console.WriteLine("Thread start");
            // Thread.Sleep(3000);
            Console.WriteLine("Thread after sleep");
            // Task.Delay(5000);
            Console.WriteLine("Thread after delay");

           // await base.Execute(signalTime);
            // WebsiteUtils.Download("https://tut.by", "tutby.txt");
        }
    }
}
