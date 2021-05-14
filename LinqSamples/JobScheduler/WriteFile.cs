using System;
using System.Text;
using System.IO;

namespace JobScheduler
{
   public static class WriteFile
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
