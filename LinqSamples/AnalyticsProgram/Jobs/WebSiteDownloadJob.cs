using System;
using JobScheduler;
using System.Net;

namespace AnalyticsProgram
{
    public class WebSiteDownloadJob : BaseJob
    {
        private readonly string _siteName;

        public bool ShouldStart { get; set; }

        public DateTime StartJob { get; set; }

        public WebSiteDownloadJob(string siteName)
            : this(siteName, DateTime.MinValue)
        {

        }

        public WebSiteDownloadJob(string siteName, DateTime timeStart)
        {
            _siteName = "https://" + siteName.Replace("https://", "");
            StartJob = timeStart;
        }

        public void Execute(DateTime signalTime)
        {
            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var reply = client.DownloadString(_siteName);

            var name = _siteName.Replace("https://", "") + ".txt";
            FileUtils.WriteToFile(name, reply);
        }
    }
}
