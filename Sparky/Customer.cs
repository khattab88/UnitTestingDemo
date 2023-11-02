using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class Customer
    {
        public int Discount = 15;

        public int OrderTotal { get; set; }
        public string Greeting { get; set; }
        public bool IsPremium { get; set; }

        public Customer()
        {
            IsPremium = false;
        }

        public string Greet(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("First name cannot be null!");
            }

            this.Discount = 20;

            this.Greeting = $"Hello, {firstName} {lastName}.";
            return this.Greeting;
        }

        public CustomerType GetCustomerDetails()
        {
            if(OrderTotal < 100)
            {
                return new BasicCustomer();
            }

            return new PremiumCustomer();
        }
    }

    public abstract class CustomerType { }
    public class BasicCustomer : CustomerType { }
    public class PremiumCustomer : CustomerType { }
}
