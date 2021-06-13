using System;
using Xunit;
using Currencies.Common.Conversion;
using FluentAssertions;
using Currencies.Common;

namespace Currencies.Tests
{
    public class CurrenciesConverterTests
    {
        [Fact]
        public void ConvertToLocal_ForExsistCurrency_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new()
            {
                Rate = 100,
                Nominal = 100,
                Date = new DateTime(2020, 12, 12),
                CharCode = "RUR",
                Id = "198"
            };
            
            //act
            var rateResult = CurrenciesConverter.ConvertToLocal(100, rate);

            //assert
           rateResult.Should().Be(100.0m);
        }
    }
}
