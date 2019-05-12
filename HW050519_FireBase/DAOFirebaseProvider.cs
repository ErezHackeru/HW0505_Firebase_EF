using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.Configuration;
using FireSharp.Response;
using Newtonsoft.Json;

namespace HW050519_FireBase
{
    class DAOFirebaseProvider : IDAOProvider
    {
        static IFirebaseClient firebaseClient;
        static IFirebaseConfig config;

        static DAOFirebaseProvider()
        {
            config = new FirebaseConfig
            {
                AuthSecret = ConfigurationManager.AppSettings["AuthSecret"],
                BasePath = ConfigurationManager.AppSettings["BasePath"]
            };

            firebaseClient = new FireSharp.FirebaseClient(config);
            if (firebaseClient != null)
            {
                Console.WriteLine("Connection Succeeded!");
            }
        }

        public bool AddCustomer(Customer customer)
        {            
            SetResponse response = firebaseClient.Set($"Customer/{customer.ID}", customer);
            Customer result = response.ResultAs<Customer>();

            Console.WriteLine("Data inserted to Customer " + customer.ID);

            if (result != null)
                return false;
            else
                return true;
        }

        public bool AddOrder(Order order)
        {
            SetResponse response = firebaseClient.Set($"Order/{order.ID}", order);
            Order result = response.ResultAs<Order>();

            Console.WriteLine("Data inserted to Order" + order.ID);

            if (result == null)
                return false;
            else
                return true;
        }

        public List<Customer> GetAllCustomers()
        {
            FirebaseResponse response = firebaseClient.Get($"Customer");

            List<Customer> result = response.ResultAs<List<Customer>>();
            result.RemoveAt(0);

            Console.WriteLine("All customer list count:");
            Console.WriteLine(result.Count);            

            return result;
        }
                
        public List<OrderCustomer> GetAllOrderCustomer()
        {
            List<Customer> customers = GetAllCustomers();
            List<Order> orders = GetAllOrders();

            var result = (from customer in customers
                          join order in orders
                          on customer.ID equals order.Customer_ID
                          select new
                          {
                              resultID = customer.ID,
                              resultPrice = order.Price,
                              resultDate = order.Date,
                              resultName = customer.Name
                          }).ToList();
            List<OrderCustomer> orderCustomers = new List<OrderCustomer>();
            int IDindex= 0;
            foreach (var r in result)
            {
                orderCustomers.Add(new OrderCustomer()
                {
                    Id = IDindex,
                    Customer_Id = r.resultID,
                    Price = (int)r.resultPrice,
                    Date = (DateTime)r.resultDate,
                    CustomerName = r.resultName
                });
                IDindex++;
            }
            Console.WriteLine("All order_customer list Order Customer:");
            Console.WriteLine(JsonConvert.SerializeObject(result));

            return orderCustomers;
        }

        public List<Order> GetAllOrders()
        {
            FirebaseResponse response = firebaseClient.Get($"Order");

            List<Order> result = response.ResultAs<List<Order>>();
            result.RemoveAt(0);

            Console.WriteLine("All order list count:");
            Console.WriteLine(result.Count);

            return result;
        }

        public List<Order> GetAllOrdersByCustomerId(int customerID)
        {            
            List<Order> result = GetAllOrders();
            result.RemoveAt(0);

            var list = (from r in result
                        where r.Customer_ID == customerID
                        select r).ToList();

            Console.WriteLine($"Order result count : {result.Count}");

            return result;
        }

        public Customer GetCustomerById(int customerId)
        {
            FirebaseResponse response = firebaseClient.Get($"Customer/{customerId}");

            Customer result = response.ResultAs<Customer>();

            Console.WriteLine(result.ID);
            Console.WriteLine(result.Name);
            
            return result;
        }

        public Order GetOrderById(int orderId)
        {
            FirebaseResponse response = firebaseClient.Get($"Customer/{orderId}");

            Order result = response.ResultAs<Order>();

            Console.WriteLine($"Order result ID : {result.ID}");
            
            return result;
        }

        public bool RemoveCustomer(int customerId)
        {
            DeleteResponse response = firebaseClient.Delete($"Customer/{customerId}");
            
            Console.WriteLine("Data Removed from Customer table: " + customerId);

            return true;            
        }

        public bool RemoveOrder(int orderId)
        {
            DeleteResponse response = firebaseClient.Delete($"Order/{orderId}");

            Console.WriteLine("Data Removed from Order table: " + orderId);

            return true;
        }

        public bool UpdateCustomer(Customer customer)
        {
            FirebaseResponse response = firebaseClient.Update($"Customer/{customer.ID}", customer);

            Customer result = response.ResultAs<Customer>();

            Console.WriteLine($"Data update in customer id: {result.ID}, name: {result.Name}");
            
            if (result == null)
                return false;
            else
                return true;
        }

        public bool UpdateOrder(Order order)
        {
            FirebaseResponse response = firebaseClient.Update($"Order/{order.ID}", order);

            Order result = response.ResultAs<Order>();

            Console.WriteLine($"Data update in order id: {result.ID}");
            
            if (result == null)
                return false;
            else
                return true;
        }
    }
}
