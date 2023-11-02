﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public double GetPrice(Customer customer) 
        {
            if (customer.IsPremium)
            {
                return Price * 0.8;
            }

            return Price;
        }
    }
}
