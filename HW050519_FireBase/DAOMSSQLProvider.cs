using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW050519_FireBase
{
    class DAOMSSQLProvider : IDAOProvider
    {
        public bool AddCustomer(Customer customer)
        {
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                cutomersNordersEntities.Customers.Add(customer);
                cutomersNordersEntities.SaveChanges();
            }
            return true;
        }

        public bool AddOrder(Order order)
        {
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                cutomersNordersEntities.Orders.Add(order);
                cutomersNordersEntities.SaveChanges();
            }
            return true;
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                customers = (from c in cutomersNordersEntities.Customers
                             select c).ToList();
                customers.ForEach(c => Console.WriteLine($"customer id {c.ID}" +
                    $" Name {c.Name}, Country {c.Country}, age {c.Age}"));
                Console.WriteLine();
            }
            return customers;
        }

        public List<OrderCustomer> GetAllOrderCustomer()
        {
            List<OrderCustomer> OrdersCustomers = new List<OrderCustomer>();
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                var result = (from c in cutomersNordersEntities.Customers
                                   join o in cutomersNordersEntities.Orders
                                   on c.ID equals o.Customer_ID
                              select new
                              {
                                  resultID = c.ID,
                                  resultPrice = o.Price,
                                  resultDate = o.Date,
                                  resultName = c.Name,
                                  resultAge = c.Age,
                                  resultCountry = c.Country
                              }).ToList();
                                
                foreach (var r in result)
                {
                    OrdersCustomers.Add(new OrderCustomer()
                    {
                        Id = r.resultID,
                        Customer_Id = r.resultID,
                        Price = (int)r.resultPrice,
                        Date = (DateTime)r.resultDate,
                        CustomerName = r.resultName,
                        CustomerAge = (int)r.resultAge,
                        CustomerCountry = r.resultCountry
                    });                    
                }

                OrdersCustomers.ForEach(oc => Console.WriteLine($"customer id {oc.Id}" +
                    $" Name {oc.CustomerName}, Country {oc.CustomerCountry}, age {oc.CustomerAge}," +
                    $" Price {oc.Price}, Date {oc.Date}, Customer_Id {oc.Customer_Id}"));
                Console.WriteLine();
            }
            return OrdersCustomers;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                orders = (from o in cutomersNordersEntities.Orders
                          select o).ToList();
                orders.ForEach(o => Console.WriteLine($"customer id {o.ID}" +
                    $" Customer Id {o.Customer_ID}, Date of order {o.Date}, price {o.Price}"));
                Console.WriteLine();
            }
            return orders;
        }

        public List<Order> GetAllOrdersByCustomerId(int customerID)
        {
            List<Order> orders = new List<Order>();
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                orders = (from o in cutomersNordersEntities.Orders
                          where o.Customer_ID == customerID
                          select o).ToList();
                Console.WriteLine($"GetAllOrdersByCustomerId(int customerID {customerID})");
                Console.WriteLine("====================================================");
                orders.ForEach(o => Console.WriteLine($"customer id {o.ID}" +
                    $" Customer Id {o.Customer_ID}, Date of order {o.Date}, price {o.Price}"));
                Console.WriteLine();
            }
            return orders;
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = new Customer();
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                customer = (from c in cutomersNordersEntities.Customers
                           where c.ID == customerId
                           select c).FirstOrDefault();
                Console.WriteLine($"GetCustomerById(int customerId {customerId})");
                Console.WriteLine("====================================================");
                Console.WriteLine(JsonConvert.SerializeObject(customer));
                Console.WriteLine();
            }
            return customer;
        }

        public Order GetOrderById(int orderId)
        {
            Order order = new Order();
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                order = (from o in cutomersNordersEntities.Orders
                            where o.ID == orderId
                         select o).FirstOrDefault();
                Console.WriteLine($"GetOrderById(int orderId {orderId})");
                Console.WriteLine("====================================================");
                Console.WriteLine(JsonConvert.SerializeObject(order));
                Console.WriteLine();
            }
            return order;
        }

        public bool RemoveCustomer(int customerId)
        {
            Customer custToRemove = GetCustomerById(customerId);
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                cutomersNordersEntities.Customers.Remove(custToRemove);
                cutomersNordersEntities.SaveChanges();
            }
            return true;
        }

        public bool RemoveOrder(int orderId)
        {
            Order custToRemove = GetOrderById(orderId);
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                cutomersNordersEntities.Orders.Remove(custToRemove);
                cutomersNordersEntities.SaveChanges();
            }
            return true;
        }

        public bool UpdateCustomer(Customer customer)
        {
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                var updateThis = (from c in cutomersNordersEntities.Customers
                                  where c.ID == customer.ID
                                  select c).FirstOrDefault();
                updateThis.Age = customer.Age;
                updateThis.Country = customer.Country;
                updateThis.Name = customer.Name;
                
                cutomersNordersEntities.SaveChanges();
            }
            return true;
        }

        public bool UpdateOrder(Order order)
        {
            using (HW0505_FirebaseEntities1 cutomersNordersEntities = new HW0505_FirebaseEntities1())
            {
                var updateThis = (from o in cutomersNordersEntities.Orders
                                  where o.ID == order.ID
                                  select o).FirstOrDefault();
                updateThis.Customer_ID = order.Customer_ID;
                updateThis.Price = order.Price;
                updateThis.Date = order.Date;

                cutomersNordersEntities.SaveChanges();
            }
            return true;
        }
    }
}
