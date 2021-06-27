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
        public async Task GetCurrencies_Always_ReturnsCurrencies()
        {
            // arrange
            using var httpTest = new HttpTest();

            var ondate = DateTime.Now;

            httpTest
                .ForCallsTo("https://www.nbrb.by/api/exrates/currencies")
                .WithVerb(HttpMethod.Get)
                .RespondWith(
                    "[{ \"Cur_ID\":1,\"Cur_ParentID\":1,\"Cur_Code\":\"008\",\"Cur_Abbreviation\":\"ALL\",\"Cur_Name\":\"Албанский лек\"" +
                    ",\"Cur_Name_Bel\":\"Албанскі лек\",\"Cur_Name_Eng\":\"Albanian Lek\",\"Cur_QuotName\":\"1 Албанский лек\"," +
                    "\"Cur_QuotName_Bel\":\"1 Албанскі лек\",\"Cur_QuotName_Eng\":\"1 Albanian Lek\",\"Cur_NameMulti\":\"\",\"Cur_Name_BelMulti\":\"\"," +
                    "\"Cur_Name_EngMulti\":\"\",\"Cur_Scale\":1,\"Cur_Periodicity\":1,\"Cur_DateStart\":\"1991-01-01T00:00:00\",\"Cur_DateEnd\":\"2007-11-30T00:00:00\"}," +
                    "{ \"Cur_ID\":2,\"Cur_ParentID\":2,\"Cur_Code\":\"012\",\"Cur_Abbreviation\":\"DZD\",\"Cur_Name\":\"Алжирский динар\"," +
                    "\"Cur_Name_Bel\":\"Алжырскі дынар\",\"Cur_Name_Eng\":\"Algerian Dinar\",\"Cur_QuotName\":\"1 Алжирский динар\"," +
                    "\"Cur_QuotName_Bel\":\"1 Алжырскі дынар\",\"Cur_QuotName_Eng\":\"1 Algerian Dinar\",\"Cur_NameMulti\":\"\",\"Cur_Name_BelMulti\":\"\"," +
                    "\"Cur_Name_EngMulti\":\"\",\"Cur_Scale\":1,\"Cur_Periodicity\":1,\"Cur_DateStart\":\"1991-01-01T00:00:00\",\"Cur_DateEnd\":\"2016-06-30T00:00:00\"}," +
                    "{ \"Cur_ID\":5,\"Cur_ParentID\":5,\"Cur_Code\":\"032\",\"Cur_Abbreviation\":\"ARS\",\"Cur_Name\":\"Аргентинское песо\"," +
                    "\"Cur_Name_Bel\":\"Аргенцінскае песа\",\"Cur_Name_Eng\":\"Argentine Peso\",\"Cur_QuotName\":\"1 Аргентинское песо\"," +
                    "\"Cur_QuotName_Bel\":\"1 Аргенцінскае песа\",\"Cur_QuotName_Eng\":\"1 Argentine Peso\",\"Cur_NameMulti\":\"\",\"Cur_Name_BelMulti\":\"\"," +
                    "\"Cur_Name_EngMulti\":\"\",\"Cur_Scale\":1,\"Cur_Periodicity\":1,\"Cur_DateStart\":\"1991-01-01T00:00:00\",\"Cur_DateEnd\":\"2016-06-30T00:00:00\"}]");

            // act
            var api = new BynCurrenciesApi();
            var result = await api.GetCurrencies();

            // assert
            var result1 = new CurrencyModel[3]
            {
               new CurrencyModel
               {
                Id = "1",
                Name = "Албанский лек",
                CharCode = "ALL",
               },
                new CurrencyModel
               {
                Id = "2",
                Name = "Алжирский динар",
                CharCode = "DZD",
               },
                new CurrencyModel
                {
                Id = "5",
                Name = "Аргентинское песо",
                CharCode = "ARS",
                },
            };
            result.Should().BeEquivalentTo(result1);
        }
        
        [Fact]
        public async Task GetCurrenciesWithDate_Always_ReturnsCurrenciesByDate()
        {
            // arrange
            using var httpTest = new HttpTest();

            var ondate = new DateTime(2021, 01, 01);

            httpTest
                .ForCallsTo("https://www.nbrb.by/api/exrates/currencies")
                .WithVerb(HttpMethod.Get)
                .WithQueryParam("date", ondate.ToString())
                .RespondWith(
                    "[{ \"Cur_ID\":1,\"Cur_ParentID\":1,\"Cur_Code\":\"008\",\"Cur_Abbreviation\":\"ALL\",\"Cur_Name\":\"Албанский лек\"" +
                    ",\"Cur_Name_Bel\":\"Албанскі лек\",\"Cur_Name_Eng\":\"Albanian Lek\",\"Cur_QuotName\":\"1 Албанский лек\"," +
                    "\"Cur_QuotName_Bel\":\"1 Албанскі лек\",\"Cur_QuotName_Eng\":\"1 Albanian Lek\",\"Cur_NameMulti\":\"\",\"Cur_Name_BelMulti\":\"\"," +
                    "\"Cur_Name_EngMulti\":\"\",\"Cur_Scale\":1,\"Cur_Periodicity\":1,\"Cur_DateStart\":\"1991-01-01T00:00:00\",\"Cur_DateEnd\":\"2007-11-30T00:00:00\"}," +
                    "{ \"Cur_ID\":2,\"Cur_ParentID\":2,\"Cur_Code\":\"012\",\"Cur_Abbreviation\":\"DZD\",\"Cur_Name\":\"Алжирский динар\"," +
                    "\"Cur_Name_Bel\":\"Алжырскі дынар\",\"Cur_Name_Eng\":\"Algerian Dinar\",\"Cur_QuotName\":\"1 Алжирский динар\"," +
                    "\"Cur_QuotName_Bel\":\"1 Алжырскі дынар\",\"Cur_QuotName_Eng\":\"1 Algerian Dinar\",\"Cur_NameMulti\":\"\",\"Cur_Name_BelMulti\":\"\"," +
                    "\"Cur_Name_EngMulti\":\"\",\"Cur_Scale\":1,\"Cur_Periodicity\":1,\"Cur_DateStart\":\"1991-01-01T00:00:00\",\"Cur_DateEnd\":\"2016-06-30T00:00:00\"}," +
                    "{ \"Cur_ID\":5,\"Cur_ParentID\":5,\"Cur_Code\":\"032\",\"Cur_Abbreviation\":\"ARS\",\"Cur_Name\":\"Аргентинское песо\"," +
                    "\"Cur_Name_Bel\":\"Аргенцінскае песа\",\"Cur_Name_Eng\":\"Argentine Peso\",\"Cur_QuotName\":\"1 Аргентинское песо\"," +
                    "\"Cur_QuotName_Bel\":\"1 Аргенцінскае песа\",\"Cur_QuotName_Eng\":\"1 Argentine Peso\",\"Cur_NameMulti\":\"\",\"Cur_Name_BelMulti\":\"\"," +
                    "\"Cur_Name_EngMulti\":\"\",\"Cur_Scale\":1,\"Cur_Periodicity\":1,\"Cur_DateStart\":\"1991-01-01T00:00:00\",\"Cur_DateEnd\":\"2016-06-30T00:00:00\"}]");

            // act
            var api = new BynCurrenciesApi();
            var result = await api.GetCurrencies(ondate);

            // assert
            var result1 = new CurrencyModel[3]
            {
               new CurrencyModel
               {
                Id = "1",
                Name = "Албанский лек",
                CharCode = "ALL",
               },
                new CurrencyModel
               {
                Id = "2",
                Name = "Алжирский динар",
                CharCode = "DZD",
               },
                new CurrencyModel
                {
                Id = "5",
                Name = "Аргентинское песо",
                CharCode = "ARS",
                },
            };
            result.Should().BeEquivalentTo(result1);
        }

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
                .ForCallsTo("https://www.nbrb.by/api/exrates/currencies")
                .WithVerb(HttpMethod.Get)
            .RespondWith("[{ \"Cur_ID\":145,\"Cur_ParentID\":145,\"Cur_Code\":" +
                          "\"840\",\"Cur_Abbreviation\":\"USD\",\"Cur_Name\":\"Доллар США\"}]");

            httpTest
                .ForCallsTo("https://www.nbrb.by/api/exrates/rates/dynamics/145")
                            .WithVerb(HttpMethod.Get)
                            .WithQueryParam("startdate", start.ToString())
                            .WithQueryParam("enddate", end.ToString())
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
                Name = "Доллар США",
                Nominal = 0,
                Rate = 2.5789,
                CharCode = "USD",
               },
                new CurrencyRateModel
               {
                Date = new DateTime(2021, 01, 02),
                Id = "145",
                Name = "Доллар США",
                Nominal = 0,
                Rate = 2.5789,
                CharCode = "USD",
               },
                new CurrencyRateModel
                {
                Date = new DateTime(2021, 01, 03),
                Id = "145",
                Name = "Доллар США",
                Nominal = 0,
                Rate = 2.5789,
                CharCode = "USD",
                },
            };

            result.Should().BeEquivalentTo(result1);
        }
    }
}
