using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Response;

namespace HW050519_FireBase
{
    class Program
    {
        static void Main(string[] args)
        {
            //FirebaseHandling();
                        
            MSSQLHandling();

            Console.ReadKey();
        }

        private static void MSSQLHandling()
        {
            Console.WriteLine("===================MSSQL Handling========================");
            IDAOProvider dAOMSSQLProvider = new DAOMSSQLProvider();
            bool IsSucceeded = false;
            ///===================
            ///Customer Handaling:
            ///===================

            Customer c2 = new Customer()
            {
                ID = 4,
                Name = "Yaakov1",
                Age = 90,
                Country = "Yoguslavia"
            };
            //IsSucceeded = dAOMSSQLProvider.AddCustomer(c2);
            //Console.WriteLine($"AddCustomer Is {IsSucceeded}");
            //dAOMSSQLProvider.GetAllCustomers();
            Customer c = dAOMSSQLProvider.GetCustomerById(1);

            Customer c3 = new Customer()
            {
                ID = 4,
                Name = "Yaakov2",
                Age = 20,
                Country = "USA"
            };
            IsSucceeded = dAOMSSQLProvider.UpdateCustomer(c3);
            Console.WriteLine($"UpdateCustomer Is {IsSucceeded}");
            IsSucceeded = false;
            Console.WriteLine();
            
            ///===================
            ///Order Handaling:
            ///===================

            Order o1 = new Order()
            {
                ID = 3,
                Customer_ID = 4,
                Price = 543,
                Date = DateTime.Now
            };
            //dAOMSSQLProvider.GetAllOrders();
            dAOMSSQLProvider.GetAllOrdersByCustomerId(2);
            Order o = dAOMSSQLProvider.GetOrderById(2);

            Order o2 = new Order()
            {
                ID = 3,
                Customer_ID = 2,
                Price = 121,
                Date = DateTime.Now
            };
            IsSucceeded = dAOMSSQLProvider.UpdateOrder(o2);
            Console.WriteLine($"UpdateOrder Is {IsSucceeded}");
            IsSucceeded = false;

            ///=========================
            ///OrderCustomer Handaling:
            ///=========================
            dAOMSSQLProvider.GetAllOrderCustomer();
            
        }

        private static void FirebaseHandling()
        {
            Console.WriteLine("===================Firebase Handling========================");
            Customer c1 = new Customer()
            {
                ID = 5,
                Name = "Yaakov1",
                Age = 90,
                Country = "Yoguslavia"
            };

            ///===================
            ///Customer Handaling:
            ///===================
            bool IsSucceeded = false;
            IDAOProvider dAOFirebaseProvider = new DAOFirebaseProvider();
            IsSucceeded = dAOFirebaseProvider.AddCustomer(c1);
            Console.WriteLine($"AddCustomer Is {IsSucceeded}");

            Customer c1Update = new Customer()
            {
                ID = 5,
                Name = "Yaakov5",
                Age = 45,
                Country = "Yoguslavia1"
            };
            IsSucceeded = dAOFirebaseProvider.UpdateCustomer(c1Update);
            Console.WriteLine($"UpdateCustomer Is {IsSucceeded}");
            //dAOFirebaseProvider.GetCustomerById(2);
            //IsSucceeded = dAOFirebaseProvider.RemoveCustomer(5);
            //Console.WriteLine($"Remove Is {IsSucceeded}");

            ///===================
            ///Order Handaling:
            ///===================
            Order o1 = new Order()
            {
                ID = 3,
                Customer_ID = 4,
                Price = 543,
                Date = DateTime.Now
            };
            //dAOFirebaseProvider.RemoveOrder(5);
            //IsSucceeded = dAOFirebaseProvider.AddOrder(o1);
            //dAOFirebaseProvider.RemoveOrder(5);
            //List<Customer> customers = dAOFirebaseProvider.GetAllCustomers();
            //List<Order> orders = dAOFirebaseProvider.GetAllOrders();
            Order o1update = new Order()
            {
                ID = 3,
                Customer_ID = 2,
                Price = 890,
                Date = DateTime.Now
            };
            IsSucceeded = dAOFirebaseProvider.UpdateOrder(o1update);
            Console.WriteLine($"UpdateOrder Is {IsSucceeded}");

            List<OrderCustomer> AllOrderCustomers = dAOFirebaseProvider.GetAllOrderCustomer();
            List<Order> orderByCustId2 = dAOFirebaseProvider.GetAllOrdersByCustomerId(2);
        }
    }
}
