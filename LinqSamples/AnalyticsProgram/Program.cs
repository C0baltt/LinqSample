using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace AnalyticsProgram
{
    public class Program
    {
        static void Main(string[] args)
        {
            var scheduler = new JobScheduler.JobScheduler(5000);

            scheduler.AddHandler(new WebSiteDoenloadJob("https://tut.by"));
            scheduler.AddHandler(new ExecutionTimeFileLoggerJob());

            scheduler.Start();

            Console.ReadKey();
        }

        private static void LogExecutionTimeInConsole(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
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
