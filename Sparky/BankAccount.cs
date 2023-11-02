using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class BankAccount
    {
        private int _balance;
        private readonly ILogger _logger;

        public BankAccount(ILogger logger)
        {
            _balance = 0;
            _logger = logger;
        }

        public bool Deposit(int amount)
        {
            _logger.Log("Deposit invoked!");

            _balance += amount;
            return true;
        }

        public bool Withdraw(int amount)
        {
            _logger.Log("Withdraw invoked!");

            if (amount <= _balance)
            {
                _balance -= amount;
                return true;
            }

            return false;
        }

        public int GetBalance() { return _balance; }
    }
}
