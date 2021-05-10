using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsProgram
{
    public class WebSiteDoenloadJob : IJob
    {
        private string _siteName;

        public WebSiteDoenloadJob(string siteName)
        {
            _siteName = siteName;
        }

        public void Execute(DateTime signalTime)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            string reply = client.DownloadString(_siteName);
            Program.WriteToFile("Stackoverflow.txt", reply);
        }
    }
}
