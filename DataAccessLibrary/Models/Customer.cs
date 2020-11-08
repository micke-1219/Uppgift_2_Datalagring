using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(int customerID, string customerName)
        {
            CustomerID = customerID;
            CustomerName = customerName;
        }

        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }
}
