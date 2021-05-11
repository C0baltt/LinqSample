using System;
using JobScheduler;
using System.Net;

namespace AnalyticsProgram
{
    public class WebSiteDownloadJob : IJob
    {
        private readonly string _siteName;

        public WebSiteDownloadJob(string siteName)
        {
            _siteName = siteName;
        }

        public void Execute(DateTime signalTime)
        {
            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var reply = client.DownloadString(_siteName);
            WriteFile.WriteToFile("WebSiteDownload.txt", reply);
        }
    }
}
