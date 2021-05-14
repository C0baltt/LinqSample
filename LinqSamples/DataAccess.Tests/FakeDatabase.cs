using System;
using System.Collections.Generic;
using AnalyticsAdapter;

namespace DataAccess.Tests
{
    public class FakeDatabase : IDatabase
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Product> Products { get; set; } = new List<Product>();
    }

}
