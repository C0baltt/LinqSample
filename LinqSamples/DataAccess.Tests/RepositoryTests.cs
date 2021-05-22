using System.Collections.Generic;
using AnalyticsAdapter;
using FluentAssertions;
using Xunit;

namespace DataAccess.Tests
{
    public class FakeDatabase : IDatabase
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Product> Products { get; set; } = new List<Product>();
    }

    [Collection("Repository")]
    public class RepositoryTests
    {
        [Fact]
        public void GetOrders_ForNonExistingCustomer_ReturnsEmptyResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);

            // act
            var orders = repository.GetOrders(1000);

            // assert
            Assert.Empty(orders);
        }

        [Fact]
        public void AddOrder_Always_AddedSuccessfully()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);
            var countBefore = db.Orders.Count;

            // act
            repository.AddOrder(1, 1);
            repository.AddOrder(1, 1);
            repository.AddOrder(1, 1);

            // assert
            var countsAfter = db.Orders.Count;
            countsAfter.Should().Be(countBefore + 3);
        }

        [Fact]
        public void GetOrders_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));

            var repository = new Repository(db);

            // act
            var orders = repository.GetOrders(1);

            // assert
            orders.Should().BeEquivalentTo(new Order(1, 1, 1));
        }
    }
}
