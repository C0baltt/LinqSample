using System.Collections.Generic;
using AnalyticsAdapter;
using FluentAssertions;
using Xunit;
using Moq;

namespace DataAccess.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void GetOrders_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            //db.Orders.Add(new Order(1, 1, 1));
            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order> { new Order(1, 1, 1) });

            //var repository = new Repository(mock.Object);
            var repository = new Repository(mock.Object);

            // act
            var orders = repository.GetOrders(1);

            // assert
            orders.Should().BeEquivalentTo(new Order(1, 1, 1));
        }

        [Fact]
        public void GetOrders_ForNonExistingCustomer_ReturnsEmptyResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            var orders = repository.GetOrders(-1);

            // assert
            orders.Should().BeEmpty();
        }

        [Fact]
        public void AddOrder_Always_AddedSuccessfully()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Products).Returns(new List<Product>());
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>());
            mock.Setup(repo => repo.Orders).Returns(new List<Order>());

            var repository = new Repository(mock.Object);

            // act
            repository.AddOrder(1, 1);
            repository.AddOrder(1, 1);
            repository.AddOrder(1, 1);

            // assert
            mock.Object.Orders.Count.Should().Be(3);
        }

        [Fact]
        public void GetMoneySpentBy_ForExistingCustomerByTwoOrders_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 500),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var moneySpentBy = repository.GetMoneySpentBy(1);

            // assert
            moneySpentBy.Should().Be(1500);
        }

        [Fact]
        public void GetAllProductsPurchased_ForExistingCustomerByThreeOrders_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1),
              new Order(3, 2, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 500),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var allProductsPurchased = repository.GetAllProductsPurchased(1);

            // assert
            allProductsPurchased.Should()
                .BeEquivalentTo(new Product(1, "Phone", 500),
                                new Product(2, "Notebook", 1000),
                                new Product(2, "Notebook", 1000));
        }

        [Fact]
        public void GetCustomerOverview_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1),
              new Order(3, 2, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 250),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var customerOverview = repository.GetCustomerOverview(1);

            // assert
            customerOverview.Should().BeEquivalentTo(new CustomerOverview
            {
                TotalMoneySpent = 2250,
                Name = "Mike",
                FavoriteProductName = "Notebook",
            });
        }

        [Fact]
        public void GetFavoriteProductName_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1),
              new Order(3, 1, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 250),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var productsPurchased = repository.GetProductsPurchased(1);

            // assert
            productsPurchased.Should()
                .BeEquivalentTo(new List<(string, int)>
                {
                    ("Phone", 2),
                    ("Notebook", 1)
                });
        }

        [Fact]
        public void AreAllPurchasesHigherThan_ForExistingCustomer_ReturnsTrue()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1),
              new Order(3, 1, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 900),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var isHigher = repository.AreAllPurchasesHigherThan(1, 899);

            // assert
            isHigher.Should().BeTrue();
        }

        [Fact]
        public void AreAllPurchasesHigherThan_ForExistingCustomer_ReturnsFalse()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1),
              new Order(3, 1, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 900),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var isHigher = repository.AreAllPurchasesHigherThan(1, 900);

            // assert
            isHigher.Should().BeFalse();
        }

        [Fact]
        public void GetTotalProductsPurchased_ForExistingProduct_ReturnsResult()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1),
              new Order(3, 1, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 900),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var totalProductsPurchased = repository.GetTotalProductsPurchased(1);

            // assert
            totalProductsPurchased.Should().Be(2);
        }

        [Fact]
        public void GetTotalProductsPurchased_ForNonExistingProduct_ReturnsZero()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1),
              new Order(3, 1, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 900),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var totalProductsPurchased = repository.GetTotalProductsPurchased(1200);

            // assert
            totalProductsPurchased.Should().Be(0);
        }

        [Fact]
        public void GetUniqueProductsPurchased_ForExistingCustomer_ReturnsArrayOfProducts()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 3, 1),
              new Order(3, 2, 1),
              new Order(4, 3, 1),
              new Order(5, 3, 2)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 900),
              new Product(2, "Notebook", 1000),
              new Product(3, "XBox", 1500)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike"),
              new Customer(2, "Nick")});

            var repository = new Repository(mock.Object);

            // act
            var productsPurchased = repository.GetUniqueProductsPurchased(1);

            // assert
            productsPurchased.Should()
                .BeEquivalentTo(new Product(1, "Phone", 900),
                                new Product(2, "Notebook", 1000),
                                new Product(3, "XBox", 1500));
        }

        [Fact]
        public void GetUniqueProductsPurchased_ForNonExistingCustomer_ReturnsZero()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 900),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var productsPurchased = repository.GetUniqueProductsPurchased(2);

            // assert
            productsPurchased.Should().BeEmpty();
        }

        [Fact]
        public void HasEverPurchasedProduct_ForExistingCustomerAndProducts_ReturnsTrue()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1),
              new Order(3, 2, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 900),
              new Product(2, "Notebook", 1000)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var productsPurchased = repository.HasEverPurchasedProduct(1, 1);

            // assert
            productsPurchased.Should().BeTrue();
        }

        [Fact]
        public void HasEverPurchasedProduct_ForExistingNonCustomerAndProducts_ReturnsFalse()
        {
            // arrange
            var mock = new Mock<IDatabase>();

            mock.Setup(repo => repo.Orders).Returns(new List<Order>
            { new Order(1, 1, 1),
              new Order(2, 2, 1)});
            mock.Setup(repo => repo.Products).Returns(new List<Product>
            { new Product(1, "Phone", 900)});
            mock.Setup(repo => repo.Customers).Returns(new List<Customer>
            { new Customer(1, "Mike")});

            var repository = new Repository(mock.Object);

            // act
            var productsPurchased = repository.HasEverPurchasedProduct(2, 2);

            // assert
            productsPurchased.Should().BeFalse();
        }
    }
}
