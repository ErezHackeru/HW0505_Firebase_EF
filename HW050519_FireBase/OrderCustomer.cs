using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW050519_FireBase
{
    class OrderCustomer
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCountry { get; set; }
        public int CustomerAge { get; set; }
    }
}
