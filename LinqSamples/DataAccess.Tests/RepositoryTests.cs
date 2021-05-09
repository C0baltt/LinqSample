using System;
using System.Collections.Generic;
using AnalyticsAdapter;
using FluentAssertions;
using Xunit;

namespace DataAccess.Tests
{
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

        [Fact]
        public void GetMoneySpentBy_ForExistingCustomerByTwoOrders_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));

            db.Products.Add(new Product(1, "Phone", 500));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            // act
            var MoneySpentBy = repository.GetMoneySpentBy(1);

            // assert
            MoneySpentBy.Should().Be(1500);
        }

        [Fact]
        public void GetAllProductsPurchased_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 2, 1));

            db.Products.Add(new Product(1, "Phone", 500));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            // act
            var AllProductsPurchased = repository.GetAllProductsPurchased(1);

            // assert
            AllProductsPurchased.Should()
                .BeEquivalentTo(new Product(1, "Phone", 500),
                                new Product(2, "Notebook", 1000),
                                new Product(2, "Notebook", 1000));
        }

        [Fact]
        public void GetAllProductsPurchased_ForNonExistingCustomer_ReturnsExeption()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 2, 1));

            db.Products.Add(new Product(1, "Phone", 500));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            // act
            Action act = () => repository.GetAllProductsPurchased(200);

            // assert
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void GetCustomerOverview_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 1, 1));

            db.Products.Add(new Product(1, "Phone", 500));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            var customerOverview = new CustomerOverview();
            customerOverview.TotalMoneySpent = 2000;
            customerOverview.Name = "Mike";
            customerOverview.FavoriteProductName = "Phone";

            // act
            var CustomerOverview = repository.GetCustomerOverview(1);

            // assert
            CustomerOverview.Should().BeEquivalentTo(customerOverview);
        }

        /*.BeEquivalentTo((CustomerOverview.Name == "Mike"),
                (CustomerOverview.TotalMoneySpent == 2000),
                (CustomerOverview.FavoriteProductName == "Phone"));*/
    }
}
