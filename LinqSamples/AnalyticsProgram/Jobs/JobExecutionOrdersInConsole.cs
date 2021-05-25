using System;
using System.Threading;
using AnalyticsAdapter;
using System.Threading.Tasks;

namespace AnalyticsProgram.Jobs
{
    public class JobExecutionOrdersInConsole : BaseJob
    {
        public DateTime StartJob { get; set; }

        public JobExecutionOrdersInConsole(DateTime timeStart)
        {
            StartJob = timeStart;
        }

        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            var repository = new Repository();
            var products = repository.GetAllPurchasesEveryCustomer();

            foreach (var item in products)
            {
                Console.Write($"Executed:{DateTime.Now}.\t");
                Console.WriteLine($"{item}");
            }
            return Task.CompletedTask;
        }
    }
}
