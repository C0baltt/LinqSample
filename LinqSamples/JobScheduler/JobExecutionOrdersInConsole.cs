using System;
using AnalyticsAdapter;

namespace JobScheduler
{
    public class JobExecutionOrdersInConsole : IJob
    {
        public bool IsFailed { get; set; }

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
