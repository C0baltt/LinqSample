using System;
using System.IO;
using System.Text;

namespace JobScheduler
{
    public static class FileUtils
    {
        public static void WriteToFile(string path, string text)
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
