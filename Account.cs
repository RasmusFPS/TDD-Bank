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
        public decimal Balance { get; private set; }
        public string Currency { get; private set; }

        //AccountNumber and Account Balance
        public Account(decimal initalbalance, string Currency)
        {
            AccountNumber = NextAccountNumber++;
            Balance = initalbalance;
            this.Currency = Currency;
        }
        //Prompts user to Deposite money with UI class
        internal void Deposit(decimal amount)
        {
            amount = UI.UserInput();

            if(amount > 0)
            {
                Balance += amount;
            }
        }
        //Prompts user to withdraw certain amount with UI class
        internal void Withdraw(decimal amount)
        {
            amount = UI.UserInput();

            if (Balance > 0 && Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                Console.WriteLine("Insufficent Funds");
            }
        }
    }
}
