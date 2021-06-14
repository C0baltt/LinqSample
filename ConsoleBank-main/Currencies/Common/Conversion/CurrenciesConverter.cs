﻿namespace Currencies.Common.Conversion
{
    public static class CurrenciesConverter
    {
        public static decimal ConvertToLocal(decimal amount, CurrencyRateModel rate)
        {
             return amount * (decimal)rate.Rate / rate.Nominal;
        }

        public static decimal ConvertFromLocal(decimal amount, CurrencyRateModel rate)
        {
            return amount / (decimal)rate.Rate * rate.Nominal;
        }
    }
}
