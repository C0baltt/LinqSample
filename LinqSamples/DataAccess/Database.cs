using System.Collections.Generic;

namespace AnalyticsAdapter
{
    public class Database : IDatabase
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Product> Products { get; set; } = new List<Product>();

        public Database()
        {
            Customers.AddRange(new []
            {
                new Customer(1, "Mike"),
                new Customer(2, "John"),
                new Customer(3, "Bob"),
                new Customer(4, "Nick"),
            });

            Products.AddRange(new []
            {
                new Product(1, "Phone", 500),
                new Product(2, "Notebook", 1000),
                new Product(3, "PC", 1500),
                new Product(4, "XBox", 800),
            });

            Orders.AddRange(new []
            {
                new Order(1, 1, 1),
                new Order(2, 1, 1),
                new Order(3, 4, 1),
                new Order(4, 2, 2),
                new Order(5, 3, 2),
                new Order(6, 4, 2),
                new Order(7, 1, 3),
                new Order(8, 2, 3),
                new Order(9, 3, 3),
                new Order(10, 3, 3),
                new Order(11, 2, 4),
                new Order(12, 3, 4),
                new Order(13, 4, 4),
            });
        }
    }
}
