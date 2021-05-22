using System;
using AnalyticsProgram.Jobs;

namespace AnalyticsProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var scheduler = new JobScheduler.JobScheduler(5000);

            // scheduler.RegisterJob(new GithubRepositoryParserJob());
            // scheduler.RegisterJob(new GithubRepositoryParserJob());
            // scheduler.RegisterJob(new GithubRepositoryParserJob());
            // scheduler.RegisterJob(new LogExecutionTimeInConsoleJob());
            // scheduler.RegisterJob(new DownloadWebsiteJob());
            scheduler.RegisterJob(new WebsiteDownloadDelayedJob(DateTime.Now.Add(TimeSpan.FromSeconds(30))));

            scheduler.Start();

            Console.ReadKey();
        }
    }
}
