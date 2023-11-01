﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class Customer
    {
        public int Discount = 15;

        public string Greeting { get; set; }

        public string Greet(string firstName, string lastName)
        {
            this.Discount = 20;

            this.Greeting = $"Hello, {firstName} {lastName}.";
            return this.Greeting;
        }
    }
}