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
        public string Currency { get; set; }

        //AccountNumber and Account Balance
        public Account(decimal initalbalance, string currency)
        {
            AccountNumber = NextAccountNumber++;
            Balance = initalbalance;
            Currency = currency;
        }
        //Prompts user to withdraw certain amount with UI class
        internal bool Withdraw(decimal amount, Account account)
        {
            if (amount > 0 && Balance >= 0)
            {
                Exchange exchange = new Exchange();

                exchange.WithdrawExchange(account, amount); //Sends in amount into Withdraw to set inSEK to correct amount.
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        internal bool Withdraw(decimal amount) 
        {
            if (amount > 0 && Balance >= 0)
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
        internal bool Deposit(decimal amount, Account account)
        {
            if (amount > 0 && Balance >= amount)
            {
                Exchange exchange = new Exchange(); 
                Balance += exchange.DepositExchange(account, amount); //Exchanges the value from Withdraw to correct currency.
                                                                      //(Amount / Currency1 = inSEK
                                                                      // return inSEK * Currency2)
                return true;
            }
            return false;
        }
    }
}
