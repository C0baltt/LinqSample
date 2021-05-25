using System;
using System.Threading.Tasks;
using AnalyticsProgram.Utils;

namespace AnalyticsProgram.Jobs
{
    public class WebsiteDownloadDelayedJob : BaseDelayedJob
    {
        private readonly string _siteName = "https://onliner.by";

        public WebsiteDownloadDelayedJob(DateTime startAt) : base(startAt)
        {
        }

        public override async Task Execute(DateTime signalTime)
        {
            await base.Execute(signalTime);
            WebsiteUtils.Download(_siteName, "onliner.by");
        }
    }
}
