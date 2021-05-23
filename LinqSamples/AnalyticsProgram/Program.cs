using System;
using JobScheduler;

namespace AnalyticsProgram
{
    public class Program
    {
        private static JobScheduler.JobScheduler s_scheduler;

        public static void Main(string[] args)
        {
            s_scheduler = new(ReadInterval());

            bool alive = true;

            while (alive)
            {
                try
                {
                    switch (GetNumberItemOfMenu())
                    {
                        case 1:
                            AddLogToConsole();
                            break;
                        case 2:
                            AddLogToFile();
                            break;
                        case 3:
                            AddDownloadWebsite();
                            break;
                        case 4:
                            AddPrintOrders();
                            break;
                        case 5:
                            AddLogToConsoleOnce();
                            break;
                        case 6:
                            AddLogToFileOnce();
                            break;
                        case 7:
                            AddDownloadWebsiteOnce();
                            break;
                        case 8:
                            AddPrintOrdersOnce();
                            break;
                        case 9:
                            Start();
                            break;
                        case 10:
                            alive = false;
                            continue;
                        default:
                            throw new ArgumentException("There is no such menu item!");
                    }
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
            }
        }

        private static int GetNumberItemOfMenu()
        {
            PrintMenu();

            int itemSelect;

            while (!int.TryParse(Console.ReadLine(), out itemSelect))
            {
                Console.Write("You entered the wrong item. Please try again: ");
            }

            return itemSelect;
        }

        private static void ShowException(Exception ex)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = color;
            Console.ReadKey();
        }

        private static void PrintMenu()
        {
            Console.Clear();
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. Add logging to the console");
            Console.WriteLine("2. Add logging to the file");
            Console.WriteLine("3. Add download website to file");
            Console.WriteLine("4. Add print orders to the console");
            Console.WriteLine("5. Add logging to console by date");
            Console.WriteLine("6. Add logging to file by date");
            Console.WriteLine("7. Add download website to file by date");
            Console.WriteLine("8. Add print orders to console by date");
            Console.WriteLine("9. Start scheduler");
            Console.WriteLine("10. Exit program");

            Console.WriteLine("Enter the item number:");
            Console.ForegroundColor = color;
        }

        private static void AddLogToConsole()
        {
            AddLogToConsoleByDate(DateTime.MinValue);
        }

        private static void AddLogToFile()
        {
            AddLogToFileByDate(DateTime.MinValue);
        }

        private static void AddDownloadWebsite()
        {
            AddDownloadWebsiteByDate(DateTime.MinValue);
        }

        private static void AddPrintOrders()
        {
            AddPrintOrdersByDate(DateTime.MinValue);
        }

        private static void AddLogToConsoleOnce()
        {
            AddLogToConsoleByDate(ReadStartDate());
        }

        private static void AddLogToFileOnce()
        {
            AddLogToFileByDate(ReadStartDate());
        }

        private static void AddDownloadWebsiteOnce()
        {
            AddDownloadWebsiteByDate(ReadStartDate());
        }

        private static void AddPrintOrdersOnce()
        {
            AddPrintOrdersByDate(ReadStartDate());
        }

        private static void AddLogToConsoleByDate(DateTime startTime)
        {
            s_scheduler.RegisterJob(new LogExecutionTimeInConsoleJob(startTime));
        }

        private static void AddLogToFileByDate(DateTime startTime)
        {
            s_scheduler.RegisterJob(new ExecutionTimeFileLoggerJob(startTime));
        }

        private static void AddDownloadWebsiteByDate(DateTime startTime)
        {
            Console.WriteLine("Input website address: ");

            var path = Console.ReadLine();

            s_scheduler.RegisterJob(new WebSiteDownloadJob(path, startTime));
        }

        private static void AddPrintOrdersByDate(DateTime startTime)
        {
            s_scheduler.RegisterJob(new JobExecutionOrdersInConsole(startTime));
        }

        private static void Start()
        {
            Console.Clear();
            Console.WriteLine("Please press Enter for start program");
            Console.WriteLine("Please press ESC for stop program");

            AwaitPress(ConsoleKey.Enter);

            s_scheduler.Start();

            AwaitStop();
        }

        private static void AwaitStop()
        {
            AwaitPress(ConsoleKey.Escape);

            s_scheduler.Stop();
        }

        private static void AwaitPress(ConsoleKey key)
        {
            while (Console.ReadKey().Key != key)
            {
            }

            Console.Clear();
        }

        private static int ReadInterval()
        {
            Console.WriteLine("Please enter the program run interval in milliseconds:");

            int interval;

            while (!int.TryParse(Console.ReadLine(), out interval))
            {
                Console.Write("You entered the wrong interval. Please try again: ");
            }

            return interval;
        }

        private static DateTime ReadStartDate()
        {
            Console.WriteLine("Please enter start date:");

            DateTime startDate;

            while (!DateTime.TryParse(Console.ReadLine(), out startDate))
            {
                Console.Write("You entered the wrong start date. Please try again: ");
            }

            return startDate;
        }
    }
}
