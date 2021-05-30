using System;

namespace JobScheduler
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }

    class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }

    public interface IConsoleWrapper
    {
        void WriteLine(string message);
    }

    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
