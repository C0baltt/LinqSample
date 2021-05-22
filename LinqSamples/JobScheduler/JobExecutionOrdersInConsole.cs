using System;
using AnalyticsAdapter;

namespace JobScheduler
{
    public class JobExecutionOrdersInConsole : IJob
    {
        public bool ShouldStart { get; set; }

        public DateTime StartJob { get; set; }

        public JobExecutionOrdersInConsole() : this(DateTime.MinValue)
        {

        }

        public JobExecutionOrdersInConsole(DateTime timeStart)
        {
            StartJob = timeStart;
        }

        public void Execute(DateTime signalTime)
        {
            var repository = new Repository();
            var products = repository.GetAllPurchasesEveryCustomer();

            foreach (var item in products)
            {
                Console.Write($"Executed:{DateTime.Now}.\t");
                Console.WriteLine($"{item}");
            }
        }
    }
}
