using System;
using Xunit;
using Currencies.Common.Conversion;
using FluentAssertions;
using Currencies.Common;

namespace Currencies.Tests
{
    public class CurrenciesConverterTests
    {

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(-4, -6, -10)]
        [InlineData(-2, 2, 0)]
        [InlineData(int.MinValue, -1, int.MaxValue)]
        public void CanAddTheory(int value1, int value2, int expected)
        {
            //arrange
            var calculator = new Calculator();
            //act
            var result = calculator.Add(value1, value2);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ConvertToLocal_ForExsistCurrency_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new()
            {
                Rate = 100,
                Nominal = 20,
            };
            
            //act
            var rateResult = CurrenciesConverter.ConvertToLocal(100, rate);

            //assert
           rateResult.Should().Be(500.0m);
        }
        
        [Fact]
        public void ConvertToLocal_FloatingRate_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new()
            {
                Rate = 2.5,
                Nominal = 3,
                Date = new DateTime(2020, 12, 12),
                CharCode = "RUR",
                Id = "198"
            };
            
            //act
            var rateResult = CurrenciesConverter.ConvertToLocal(2.5M, rate);

            //assert
           rateResult.Should().Be(2.0833333333333333333333333333M);
        }
        
        [Fact] 
        public void ConvertToLocal_ZeroRate_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new()
            {
                Rate = 0,
                Nominal = 3,
                Date = new DateTime(2020, 12, 12),
                CharCode = "RUR",
                Id = "198"
            };
            
            //act
            var rateResult = CurrenciesConverter.ConvertToLocal(0, rate);

            //assert
           rateResult.Should().Be(0);
        }

        [Fact]
        public void ConvertToLocal_ForNegativeAmount_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new()
            {
                Rate = 2,
                Nominal = 5,
                Date = new DateTime(2020, 12, 12),
                CharCode = "RUR",
                Id = "198"
            };

            //act
            var rateResult = CurrenciesConverter.ConvertToLocal(-50, rate);

            //assert
            rateResult.Should().Be(-20M);
        }

        [Fact]
        public void ConvertFromLocal_ForExsistCurrency_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new()
            {
                Rate = 100,
                Nominal = 20,
                Date = new DateTime(2020, 12, 12),
                CharCode = "RUR",
                Id = "198"
            };
            
            //act
            var rateResult = CurrenciesConverter.ConvertFromLocal(20, rate);

            //assert
           rateResult.Should().Be(4.0m);
        }
    
        [Fact]
        public void ConvertFromLocal_FloatingRate_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new()
            {
                Rate = 2.5,
                Nominal = 3,
                Date = new DateTime(2020, 12, 12),
                CharCode = "RUR",
                Id = "198"
            };
            
            //act
            var rateResult = CurrenciesConverter.ConvertFromLocal(4m, rate);

            //assert
           rateResult.Should().Be(4.8m);
        }
    
        [Fact]
        public void ConvertFromLocal_ZeroRate_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new()
            {
                Rate = 2.5,
                Nominal = 0,
                Date = new DateTime(2020, 12, 12),
                CharCode = "RUR",
                Id = "198"
            };
            
            //act
            var rateResult = CurrenciesConverter.ConvertFromLocal(0, rate);

            //assert
           rateResult.Should().Be(0);
        }
    
        [Fact]
        public void ConvertFromLocal_ForNegativeAmount_ReturnResult()
        {
            //arrange
            CurrencyRateModel rate = new()
            {
                Rate = 2,
                Nominal = 8,
                Date = new DateTime(2020, 12, 12),
                CharCode = "RUR",
                Id = "198"
            };
            
            //act
            var rateResult = CurrenciesConverter.ConvertFromLocal(-50, rate);

            //assert
           rateResult.Should().Be(-200);
        }
    }
}
