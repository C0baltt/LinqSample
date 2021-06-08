using System.Threading.Tasks;
using Currencies.Entities;

namespace Currencies.Common
{
    public class CurrencyRateUtils
    {
        public async Task<CurrencyRate> GetNewCurrencyRate()
        {
            var newRate = await _currenciesApi.GetCurrencyRate(currencyAbbreviation, onDate);
            AddToCache(date, newRate);
            return newRate;
        }
    }
}
