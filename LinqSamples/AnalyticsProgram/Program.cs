using System;
using JobScheduler;

namespace AnalyticsProgram
{
    public class Program
    {
        static void Main(string[] args)
        {
            var scheduler = new JobScheduler.JobScheduler(5000);

            scheduler.AddHandler(new WebSiteDownloadJob("https://tut.by"));
            scheduler.AddHandler(new ExecutionTimeFileLoggerJob()); 
            scheduler.AddHandler(new WriteToConsole()); 

            scheduler.Start();

            Console.ReadKey();
        }
    }
}
