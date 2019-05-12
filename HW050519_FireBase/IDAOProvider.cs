using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW050519_FireBase
{
    interface IDAOProvider
    {
        List<Customer> GetAllCustomers();
        List<Order> GetAllOrders();
        List<Order> GetAllOrdersByCustomerId(int customerID);
        Order GetOrderById(int orderId);
        Customer GetCustomerById(int customerId);
        bool AddCustomer(Customer customer);
        bool RemoveCustomer(int customerId);
        bool UpdateCustomer(Customer customer);
        bool AddOrder(Order order);
        bool RemoveOrder(int orderId);
        bool UpdateOrder(Order order);
        List<OrderCustomer> GetAllOrderCustomer();
    }
}
