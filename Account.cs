using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Account
    {
        private static int NextAccountNumber = 1000;

        internal int AccountNumber { get; private set; }
        internal decimal Balance { get; set; }//Removed private to be able to use it in banktransfer class.
        internal string Currency { get; set; }

        //AccountNumber and Account Balance
        internal Account(decimal initalbalance, string currency)
        {
            AccountNumber = NextAccountNumber++;
            Balance = initalbalance;
            Currency = currency;
        }
        internal bool Withdraw(decimal amount) 
        {
            if (amount > 0 && Balance >= amount)
            {

                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Prompts user to Deposite money with UI class
        internal bool Deposit(decimal amount)
        {

            if (amount > 0)
            {
                Balance += amount;

                return true;
            }
            return false;
        }
    }
}
