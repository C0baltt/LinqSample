using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace AnalyticsProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var scheduler = new JobScheduler.JobScheduler(5000);

            scheduler.AddHandler(LogExecutionTimeInConsole);
            scheduler.AddHandler(LogExecutionTimeInFile);
            scheduler.AddHandler((_) => DownloadWebsite());

            scheduler.Start();

            Console.ReadKey();
        }

        private static void LogExecutionTimeInConsole(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
        }

        private static void LogExecutionTimeInFile(DateTime signalTime)
        {
            WriteToFile("ExecutionTimeLog.txt", signalTime.ToString(CultureInfo.InvariantCulture));
        }

        private static void DownloadWebsite()
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            string reply = client.DownloadString("https://stackoverflow.com/questions/26233/fastest-c-sharp-code-to-download-a-web-page");

            WriteToFile("Stackoverflow.txt", reply);
        }

        private static void WriteToFile(string path, string text)
        {
            if (!File.Exists(path))
            {
                using var stream = File.Create(path);
                byte[] info = new UTF8Encoding(true).GetBytes(text);
                stream.Write(info, 0, info.Length);
            }
            else
            {
                File.AppendAllText(path, text);
                File.AppendAllText(path, Environment.NewLine);
            }
        }
    }
}
