using System;
using Xunit;
using Currencies;
using Currencies.Common.Conversion;
using FluentAssertions;
using Currencies.Common;
using static System.Collections.IEnumerable
    ;

namespace Currencies.Tests
{
    public class CurrenciesConverterTests
    {
        [Fact]
        public void ConvertToLocal_ForExsistCurrency_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new();
            rate.Rate = 100;
            rate.Nominal = 100;
            rate.Date = new DateTime(2020,12,12);
            rate.CharCode = "RUR";
            rate.Id = 198;


            //act
            var rateResult = CurrenciesConverter.ConvertToLocal(100, rate);

            //assert
            
        }
    }
}
