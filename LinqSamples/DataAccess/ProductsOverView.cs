using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyticsAdapter
{
    public class ProductsOverView
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Amount { get; set; }

        public ProductsOverView(string customerName, string productName, decimal productPrice, int amount)
        {
            CustomerName = customerName;
            ProductName = productName;
            ProductPrice = productPrice;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"Customer name: {CustomerName}\tProduct name: {ProductName}\tProduct price: {ProductPrice}\tProduct amount: {Amount}\n";
        }
    }
}
