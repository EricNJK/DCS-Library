using DCSLibrary;
using DCSLibrary.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TestZone
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTests();
            RunTests2();
            Console.WriteLine("\t--- Press return to exit ---");
            Console.ReadKey();
            return;
        }

        private static void RunTests()
        {   //Test - ManagerDAO.Authenticate()
            LocalManagerDAO managerDAO = new LocalManagerDAO();
            LocalManager manager = managerDAO.Authenticate("11112222", "0000");
            if (manager != null)
                Console.WriteLine(manager.Name + " - " + manager.Store);
            else
                Console.WriteLine("Null!");

            /*//Test - Product::Equals()
            Product a = new Product(1, 50.0f);
            Product b = new Product(1, 50.0f);
            Product c = new Product(3, 50.0f);

            Console.WriteLine(a.Equals(b));
            Console.WriteLine(a.Equals(c));
            */

            Dictionary<int, Product> beautyProducts = new Dictionary<int, Product>();
            //Test - ManagerDAO::GetListOfProducts()
            Dictionary<int, Product> pharmProducts = managerDAO.GetListOfProducts(ProductType.PHARMACEUTICALS);
            beautyProducts = managerDAO.GetListOfProducts(ProductType.BEAUTY);
            
            //Test - AddProdtoStock
            //managerDAO.AddProductToStock("Vaseline Fresh", "Perfumed, for men", ProductType.BEAUTY, float.Parse("70.50"), 3);
            beautyProducts = managerDAO.GetListOfProducts(ProductType.BEAUTY);   //Update Beauty list
            Order order = new Order(PaymentMethod.CASH, manager.Store, 2);
            //Test - Order::AddProduct()
            order.AddProduct(beautyProducts);     //New from code
            order.AddProduct(beautyProducts[1]);  //From database
            //Test - RecordLocalOrder
            managerDAO.RecordLocalOrder(order, manager.Store);
            return;
        }

        private static void RunTests2()
        {
            
        }
    }
}
