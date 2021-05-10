using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalyticsAdapter
{
    public class Repository : IRepository
    {
        private readonly IDatabase _db;

        public Repository(IDatabase db)
        {
            _db = db;
        }

        public void AddOrder(int customerId, int productId)
        {
            _db.Orders.Add(new Order(_db.Orders.Count + 1, customerId, productId));
        }

        public Order[] GetOrders(int customerId)
        {
            return GetOrdersInternal(customerId).ToArray();
        }

        public Order GetOrder(int orderId)
        {
            var order = _db.Orders.SingleOrDefault(order => order.Id == orderId);
            if (order == null)
            {
                throw new InvalidOperationException();
            }

            return order;
        }

        public decimal GetMoneySpentBy(int customerId)
        {
            return _db.Orders.Join(_db.Products,
                    (o) => o.ProductId,
                    (p) => p.Id,
                    (o, p) => new {p.Price, o.CustomerId})
                .Where(x => x.CustomerId == customerId)
                .Sum(x => x.Price);
        }

        public Product[] GetAllProductsPurchased(int customerId)
        {
            return GetOrders(customerId)
                .Join(_db.Products,
                o => o.ProductId,
                p => p.Id,
                (o, p) => p)
                .ToArray();

            throw new InvalidOperationException();
        }

        public CustomerOverview GetCustomerOverview(int customerId)
        {
            var name = _db.Customers.Single(x => x.Id == customerId).Name;

            return new CustomerOverview
            {
                Name = name,
                TotalMoneySpent = GetMoneySpentBy(customerId),
                FavoriteProductName = GetFavoriteProductName(customerId),
            };
        }

        public List<(string productName, int numberOfPurchases)> 
            GetProductsPurchased(int customerId)
        {
            return GetProductOrdersJoined(customerId)
                .GroupBy(x => x.order.ProductId)
                .Select(g => (g.First().product.Name, g.Count()))
                .ToList();
        }

        private string GetFavoriteProductName(int customerId)
        {
            return GetOrdersInternal(customerId)
                .Join(_db.Products,
                    (o) => o.ProductId,
                    (p) => p.Id,
                    (o, p) => new
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                    })
                .GroupBy(x => x.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Count = g.Count(),
                    ProductName = g.First().ProductName
                })
                .OrderBy(x => x.Count)
                .Last()
                .ProductName;
        }

        public bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice)
        {
            return GetProductOrdersJoined(customerId)
                .All(x => x.product.Price > targetPrice);
        }

        public int GetTotalProductsPurchased(int productId)
        {
            return _db.Orders
                .Where(x => x.ProductId == productId)
                .Count();

            throw new InvalidOperationException();
        }

        public bool HasEverPurchasedProduct(int customerId, int productId)
        {
            return GetOrders(customerId)
                .Any(x => x.ProductId == productId);
        }

        public Product[] GetUniqueProductsPurchased(int customerId)
        {
            return GetProductOrdersJoined(customerId)
                .Select(x => x.product)
                .Distinct()
                .ToArray();
        }

        public bool DidPurchaseAllProducts(int customerId, params int[] productIds)
        {
            return GetOrders(customerId)
                .Select(o => o.ProductId)
                .Distinct()
                .Intersect(productIds)
                .Count() == productIds.Count();
        }

        private IEnumerable<Order> GetOrdersInternal(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId);
        }

        private IEnumerable<(Product product, Order order)> GetProductOrdersJoined(int customerId)
        {
            return GetOrdersInternal(customerId).Join(_db.Products,
                (o) => o.ProductId,
                (p) => p.Id,
                (o, p) => (p, o));
        }
    }

    public interface IRepository
    {
        void AddOrder(int customerId, int productId);

        Order[] GetOrders(int customerId);

        Order GetOrder(int orderId);

        decimal GetMoneySpentBy(int customerId);

        Product[] GetAllProductsPurchased(int customerId);

        Product[] GetUniqueProductsPurchased(int customerId);

        int GetTotalProductsPurchased(int productId);

        bool HasEverPurchasedProduct(int customerId, int productId);

        bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice);

        bool DidPurchaseAllProducts(int customerId, params int[] productIds);

        CustomerOverview GetCustomerOverview(int customerId);

        List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId);
    }
}
