using System.Collections.Generic;

namespace AnalyticsAdapter
{
    public interface IDatabase
    {
        List<Customer> Customers { get; set; }
        List<Order> Orders { get; set; }
        List<Product> Products { get; set; }
    }
}
