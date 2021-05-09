using AnalyticsAdapter;
using FluentAssertions;
using System;
using Xunit;

namespace DataAccessTests
{
   public class RepositoryTests
    {
        private readonly Database _db = new Database();

        [Fact]
        public void GetOrders_ForNonExistingCustomer_ReturnEmptyResult()
        {
            //arrange
            var repository = new Repository(_db);

            //act
            var orders = repository.GetOrders(1000);

            //assert

            Assert.Empty(orders);
        }

        [Fact]
        public void GetOrders_ForExistingCustomer_ReturnResult()
        {
            //arrange
            var db = new Database();
            var repository = new Repository(db);

            //act
            var orders = repository.GetOrders(1);

            //assert

            orders.Should().BeEquivalentTo(new[]
            {
                new Order(1, 1, 1),
                new Order(2, 1, 1),
                new Order(3, 4, 1),
            });
        }
    }
}
