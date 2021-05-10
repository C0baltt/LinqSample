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
        public void GetAllProductsPurchased_ForExistingCustomerByThreeOrders_ReturnsResult()
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
            var allProductsPurchased = repository.GetAllProductsPurchased(1);

            // assert
            allProductsPurchased.Should()
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

            db.Products.Add(new Product(1, "Phone", 250));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            // act
            var customerOverview = repository.GetCustomerOverview(1);

            // assert
            var customerOverviewSample = new CustomerOverview();
            customerOverviewSample.TotalMoneySpent = 1500;
            customerOverviewSample.Name = "Mike";
            customerOverviewSample.FavoriteProductName = "Phone";

            customerOverview.Should().BeEquivalentTo(customerOverviewSample);
        }
        
        [Fact]
        public void GetFavoriteProductName_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 1, 1));

            db.Products.Add(new Product(1, "Phone", 250));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            // act
            var productsPurchased = repository.GetProductsPurchased(1);

            // assert
            var productsPurchasedSample = new List<(string, int)>
            {
                ("Phone", 2),
                ("Notebook", 1)
            };

            productsPurchased.Should()
                .BeEquivalentTo(productsPurchasedSample);
        }
        
        [Fact]
        public void AreAllPurchasesHigherThan_ForExistingCustomer_ReturnsTrue()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 1, 1));

            db.Products.Add(new Product(1, "Phone", 900));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            // act
            var isHigher = repository.AreAllPurchasesHigherThan(1, 899);

            // assert
            var productsPurchasedSample = new List<(string, int)>
            {
                ("Phone", 2),
                ("Notebook", 1)
            };

            isHigher.Should().Be(true);
        }
         
        [Fact]
        public void AreAllPurchasesHigherThan_ForExistingCustomer_ReturnsTFalse()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 1, 1));

            db.Products.Add(new Product(1, "Phone", 900));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            // act
            var isHigher = repository.AreAllPurchasesHigherThan(1, 900);

            // assert
            var productsPurchasedSample = new List<(string, int)>
            {
                ("Phone", 2),
                ("Notebook", 1)
            };

            isHigher.Should().Be(false);
        }

      [Fact]
        public void GetTotalProductsPurchased_ForExistingProduct_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 1, 1));

            db.Products.Add(new Product(1, "Phone", 900));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            // act
            var totalProductsPurchased = repository.GetTotalProductsPurchased(1);

            // assert
            totalProductsPurchased.Should().Be(2);
        }

        [Fact]
        public void GetUniqueProductsPurchased_ForExistingCustomer_ReturnsArrayOfProducts()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 3, 1));
            db.Orders.Add(new Order(4, 2, 1));
            db.Orders.Add(new Order(5, 1, 2));

            db.Products.Add(new Product(1, "Phone", 900));
            db.Products.Add(new Product(2, "Notebook", 1000));
            db.Products.Add(new Product(3, "XBox", 1500));

            db.Customers.Add(new Customer(1, "Mike"));
            db.Customers.Add(new Customer(2, "Nick"));

            var repository = new Repository(db);

            // act
            var totalProductsPurchased = repository.GetUniqueProductsPurchased(1);

            // assert
            totalProductsPurchased.Should().Be(0);
        }
        
      [Fact]
        public void GetTotalProductsPurchased_ForNonExistingProduct_ReturnsZero()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1, 1, 1));
            db.Orders.Add(new Order(2, 2, 1));
            db.Orders.Add(new Order(3, 1, 1));

            db.Products.Add(new Product(1, "Phone", 900));
            db.Products.Add(new Product(2, "Notebook", 1000));

            db.Customers.Add(new Customer(1, "Mike"));

            var repository = new Repository(db);

            // act
            var totalProductsPurchased = repository.GetTotalProductsPurchased(1200);

            // assert
            totalProductsPurchased.Should().Be(0);
        }

        
    }
}
