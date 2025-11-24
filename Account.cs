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

        public int AccountNumber { get; private set; }
        public decimal Balance { get; set; }//Removed private to be able to use it in banktransfer class.
        public string Currency { get; private set; }

        //AccountNumber and Account Balance
        public Account(decimal initalbalance, string Currency)
        {
            AccountNumber = NextAccountNumber++;
            Balance = initalbalance;
            this.Currency = Currency;
        }
        //Prompts user to Deposite money with UI class
        internal bool Deposit(decimal amount)
        {
            //amount = UI.UserInput(); //Might be better to remove this so it can be re-used in
            //banktransfer and call on it separately in Main

            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }
        //Prompts user to withdraw certain amount with UI class
        internal bool Withdraw(decimal amount)
        {
            if (Balance > 0 && Balance >= amount)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                Console.WriteLine("Insufficent Funds");
                return false;
            }
        }
    }
}
