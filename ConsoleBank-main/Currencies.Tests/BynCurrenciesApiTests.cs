using System;
using System.Net.Http;
using System.Threading.Tasks;
using Currencies.Apis.Byn;
using Currencies.Common;
using FluentAssertions;
using Flurl.Http.Testing;
using Xunit;

namespace Currencies.Tests
{
    public class BynCurrenciesApiTests
    {
        [Fact]
        public async Task GetCurrencyRate_Always_ReturnsRate()
        {
            // arrange
            using var httpTest = new HttpTest();

            var ondate = DateTime.Now;

            httpTest
                .ForCallsTo("https://www.nbrb.by/api/exrates/rates/USD")
                .WithVerb(HttpMethod.Get)
                .WithQueryParam("parammode", 2)
                .WithQueryParam("ondate", ondate.ToString())
                .RespondWith(
                    "{\"Cur_ID\":145,\"Date\":\"2021-06-15T00:00:00\"" +
                    ",\"Cur_Abbreviation\":\"USD\",\"Cur_Scale\":1,\"Cur_Name\":" +
                    "\"Доллар США\",\"Cur_OfficialRate\":2.4892}");

            // act
            var api = new BynCurrenciesApi();
            var result = await api.GetCurrencyRate("USD", ondate);

            // assert
            result.Should().BeEquivalentTo(new CurrencyRateModel
            {
                Date = ondate,
                Nominal = 1,
                Rate = 2.4892,
                Id = "145",
                Name = "Доллар США",
                CharCode = "USD"
            });
        }

        [Fact]
        public async Task GetDynamics_Always_ReturnDynamics()
        {
            // arrange
            using var httpTest = new HttpTest();

            var ondate = DateTime.Now;
            string charCode = "USD";
            DateTime start = new DateTime(2021, 01, 01);
            DateTime end = new DateTime(2021, 01, 03);
        //https://www.nbrb.by/API/ExRates/Rates/Dynamics/145?startDate=2021-1-1&endDate=2021-1-3
            httpTest
                .ForCallsTo("https://www.nbrb.by/API/ExRates/Rates/Dynamics")
                .WithVerb(HttpMethod.Get)
                .RespondWith(
                    "[{ \"Cur_ID\":145,\"Date\":\"2021-01-01T00:00:00\",\"Cur_OfficialRate\":2.5789}," +
                    "{ \"Cur_ID\":145,\"Date\":\"2021-01-02T00:00:00\",\"Cur_OfficialRate\":2.5789}," +
                    "{ \"Cur_ID\":145,\"Date\":\"2021-01-03T00:00:00\",\"Cur_OfficialRate\":2.5789}]");

            // act
            var api = new BynCurrenciesApi();
            var result = await api.GetDynamics(charCode, start, end);

            // assert
            var result1 = new CurrencyRateModel[3]
            {
               new CurrencyRateModel
               {
                Date = new DateTime(2021, 01, 01),
                Id = "145",
                Name = "USD",
                Nominal = 1,
                Rate = 2.5789,
                CharCode = "USD",
               },
                new CurrencyRateModel
               {
                   Date = new DateTime(2021, 01, 02),
                Id = "USD",
                Name = "USD",
                Nominal = 1,
                Rate = 2.4892,
                CharCode = "USD",
               },
                new CurrencyRateModel
               {
                   Date = new DateTime(2021, 01, 03),
                Id = "USD",
                Name = "USD",
                Nominal = 1,
                Rate = 2.4892,
                CharCode = "USD",
               },
            };

            result.Should().BeEquivalentTo(result1);
        }
    }
}
