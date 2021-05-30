using System;
using System.Threading.Tasks;
using Currencies.Entities;
using Flurl;
using Flurl.Http;

namespace Currencies
{
    public class CurrenciesApi : ICurrenciesApi
    {
    //https://www.nbrb.by/api/exrates/rates/USD?parammode=2 – по буквенному коду валюты (ИСО 4217)

    // Полный перечень иностранных валют, по отношению к которым
    // Национальным банком устанавливается официальный курс белорусского рубля:
    //Адрес запроса: https://www.nbrb.by/api/exrates/currencies
    private const string CurrenciesApiUrl = "https://www.nbrb.by/api/exrates/currencies";
        //                                      "https://www.nbrb.by/api/exrates/currencies"

        //        Официальный курс белорусского рубля по отношению к иностранным валютам, устанавливаемый
        //        Национальным банком на конкретную дату:
        //Адрес запроса:                            https://www.nbrb.by/api/exrates/rates
        private const string CurrencyRatesApiUrl = "https://www.nbrb.by/api/exrates/rates";
        private const string Parammode2 = "?parammode=2";

        public Task<Currency[]> GetCurrencies()
        {
            return CallApi(() => CurrenciesApiUrl.GetJsonAsync<Currency[]>());
        }

        public Task<CurrencyRate> GetCurrencyRate(int currencyId)
        {
            return CallApi(() => CurrencyRatesApiUrl.AppendPathSegment(currencyId).GetJsonAsync<CurrencyRate>());
        }
        
        public Task<CurrencyRate> GetCurrencyRate(string abbreviation)
        {
            //return CallApi(() => CurrencyRatesApiUrl.AppendPathSegment(abbreviation+Parammode2).GetJsonAsync<CurrencyRate>());
            return CallApi(() => CurrencyRatesApiUrl
                .AppendPathSegment(abbreviation)
                .SetQueryParams(new
                {
                    parammode = 2,
                })
                .GetJsonAsync<CurrencyRate>());
        }
        
        private static async Task<T> CallApi<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (FlurlHttpException e) when (e.StatusCode == 404)
            {
                throw new CurrencyNotAvailableException("Currency not available");
            }
        }
    }
}
