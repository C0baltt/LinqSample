using System;
using System.Threading.Tasks;
using AnalyticsProgram.Utils;

namespace AnalyticsProgram.Jobs
{
    public class WebSiteDownloadJob : BaseJob
    {
        private const string FilePath = "Stackoverflow.txt";
        private string _siteName = "https://stackoverflow.com/questions/26233/fastest-c-sharp-code-to-download-a-web-page";

        public DateTime StartJob { get; set; }

        public WebSiteDownloadJob(string siteName, DateTime timeStart)
        {
            _siteName = "https://" + siteName.Replace("https://", "");
            StartJob = timeStart;
        }

        public override Task Execute(DateTime signalTime)
        {
            WebsiteUtils.Download(_siteName, FilePath);
            return Task.CompletedTask;
        }
    }
}
