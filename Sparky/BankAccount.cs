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
            _logger.Log("Deposit invoked again!");
            _logger.LogSeverity = 101;

            _balance += amount;
            return true;
        }

        public bool Withdraw(int amount)
        {
            if (amount <= _balance)
            {
                _logger.LogToDb("Withdrawl Amount: " + amount.ToString());

                _balance -= amount;
                return _logger.LogBalanceAfterWithdrawal(_balance);
            }

            return _logger.LogBalanceAfterWithdrawal(_balance - amount);
        }

        public int GetBalance() { return _balance; }
    }
}
