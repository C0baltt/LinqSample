using System;
using AnalyticsAdapter;
class Program
{
    private static Database database = new();
    static void Main(string[] args)
    {
        bool alive = true;
        while (alive)
        {
            Console.WriteLine();
            Console.WriteLine("1. GetOrders                  \t 2. GetOrder                \t 3. GetAllProductsPurchased");
            Console.WriteLine("4. GetUniqueProductsPurchased \t 5. HasEverPurchasedProduct \t 6. AreAllPurchasesHigherThan");
            Console.WriteLine("7. DidPurchaseAllProducts     \t 8. GetMoneySpentBy         \t 9. Exit program ");
            Console.WriteLine("Enter the item number:");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        GetOrders();
                        break;
                    case 2:
                        GetOrder();
                        break;
                    case 3:
                        GetAllProductsPurchased();
                        break;
                    case 4:
                        GetUniqueProductsPurchased();
                        break;
                    case 5:
                        HasEverPurchasedProduct();
                        break;
                    case 6:
                        AreAllPurchasesHigherThan();
                        break;
                    case 7:
                        DidPurchaseAllProducts();
                        break;
                    case 8:
                        GetMoneySpentBy();
                        break;
                    case 9:
                        alive = false;
                        continue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    private static void GetOrders()
    {
        Console.WriteLine("Enter customerId: ");
        int customerId = Convert.ToInt32(Console.ReadLine());
        database.GetOrders(customerId);
    }
    private static void GetOrder()
    {
        Console.WriteLine("Enter orderId: ");
        int orderId = Convert.ToInt32(Console.ReadLine());
        database.GetOrder(orderId);
    }
    private static void GetMoneySpentBy()
    {
        Console.WriteLine("Enter customerId: ");
        int customerId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"{database.GetMoneySpentBy(customerId)}");
    }
    private static void GetAllProductsPurchased()
    {
        Console.WriteLine("Enter customerId: ");
        int customerId = Convert.ToInt32(Console.ReadLine());
        database.GetAllProductsPurchased(customerId);
    }
    private static void GetUniqueProductsPurchased()
    {
        Console.WriteLine("Enter customerId: ");
        int customerId = Convert.ToInt32(Console.ReadLine());
        database.GetUniqueProductsPurchased(customerId);
    }
    private static void HasEverPurchasedProduct()
    {
        Console.WriteLine("Enter customerId: ");
        int customerId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter productId: ");
        int productId = Convert.ToInt32(Console.ReadLine());
        database.HasEverPurchasedProduct(customerId, productId);
    }
    private static void AreAllPurchasesHigherThan()
    {
        Console.WriteLine("Enter customerId: ");
        int customerId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter targetPrice: ");
        decimal targetPrice = Convert.ToDecimal(Console.ReadLine());
        Console.WriteLine($"{database.AreAllPurchasesHigherThan(customerId, targetPrice)}");
    }
    private static void DidPurchaseAllProducts()
    {
        Console.WriteLine("Enter customerId: ");
        int customerId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter size of array: ");
        int size = Convert.ToInt32(Console.ReadLine());
        int[] productIds = new int[size];
        for (int i = 0; i < size; i++)
        {
            Console.WriteLine("Enter productId: ");
            int productId = Convert.ToInt32(Console.ReadLine());
            productIds[i] = productId;
        }
        Console.WriteLine($"{database.DidPurchaseAllProducts(customerId, productIds)}");
    }
}