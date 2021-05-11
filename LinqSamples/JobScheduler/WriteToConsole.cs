using System;

namespace JobScheduler
{
    public class WriteToConsole : IJob
    {
        public void Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
        }
    }
}
