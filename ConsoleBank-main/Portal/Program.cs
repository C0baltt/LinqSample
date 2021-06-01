using System;
using System.Threading.Tasks;
using Currencies;
using Currencies.Entities;

namespace Portal
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var api = new CurrenciesApi();
            Currency[] currencies = await api.GetCurrencies();

            foreach (var currency in currencies)
            {
                Console.WriteLine(currency);
            }

            var rate = await api.GetCurrencyRate("IDR");
            Console.WriteLine(rate);

            var rateOnDate = await api.GetCurrencyRateOnDate("IDR");
            Console.WriteLine(rateOnDate);
        }
    }
}
