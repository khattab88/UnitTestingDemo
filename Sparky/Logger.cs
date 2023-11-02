using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public interface ILogger
    {
        public int LogSeverity { get; set; }
        public string LogType { get; set; }

        void Log(string message);
        bool LogToDb(string message);
        bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal);
        string LogWithReturnString(string message);
        bool LogWithOutputResult(string message, out string outputResult);
    }

    public class Logger : ILogger
    {
        public int LogSeverity { get;  set; }
        public string LogType { get; set; }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal)
        {
            if(balanceAfterWithdrawal >= 0) 
            {
                Console.WriteLine("Withdrawal: Success");
                return true;
            }

            Console.WriteLine("Withdrawal: Failed");
            return false;
        }

        public bool LogToDb(string message)
        {
            Console.WriteLine($"Log to Db: {message}");
            return true;
        }

        public bool LogWithOutputResult(string message, out string outputResult)
        {
            outputResult = "Log: " + message;
            return true;
        }

        public string LogWithReturnString(string message)
        {
            Console.WriteLine(message);
            return message.ToLower();
        }
    }

    //public class FakeLogger : ILogger
    //{
    //    public void Log(string message)
    //    {
    //        // NULL Object
    //    }

    //    public bool LogBalanceAfterWithdrawal(int balanceAfterWithdrawal)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool LogToDb(string message)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public string LogWithReturnString(string message)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
