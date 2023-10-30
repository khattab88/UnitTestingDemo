using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class Person
    {
        public string Greeting { get; set; }

        public string Greet(string firstName, string lastName)
        {
            this.Greeting = $"Hello, {firstName} {lastName}.";
            return this.Greeting;
        }
    }
}
