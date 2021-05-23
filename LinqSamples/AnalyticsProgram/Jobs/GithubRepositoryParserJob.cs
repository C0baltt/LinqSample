using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
namespace AnalyticsProgram.Jobs
{
    public class GithubRepositoryParserJob : BaseJob
    {
        private static readonly HttpClient Client = new();

        public GithubRepositoryParserJob()
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

       
        public override async Task Execute(DateTime signalTime)
        {
            var result = await ProcessRepositories();
            Console.WriteLine(result);
        }

        static async Task JsonJob(string[] args)
        {
            // сохранение данных
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                
                await JsonSerializer.SerializeAsync<Person>(fs, tom);
                Console.WriteLine("Data has been saved to file");
            }

            // чтение данных
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                Person restoredPerson = await JsonSerializer.DeserializeAsync<Person>(fs);
                Console.WriteLine($"Name: {restoredPerson.Name}  Age: {restoredPerson.Age}");
            }
        }



        public override async Task<bool> ShouldRun(DateTime signalTime)
        {
            string isConnected = await Client.GetStringAsync("https://api.github.com");
            return await base.ShouldRun(signalTime) && isConnected.Length > 0;
        }

        private static async Task<string> ProcessRepositories()
        {
                var stringTask = Client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            return await stringTask;
        }
    }
}
