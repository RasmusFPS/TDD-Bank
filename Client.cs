using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Client : User
    {
        internal List<Account> Accounts { get; private set; }
        public Client(string username, string password, bool isAdmin) : base(username, password, false)
        {
            //new makes sure the account is fresh and created for the object
            Accounts = new List<Account>();
        }
        
        //This takes user input on how much money should be in the account
        public void CreateNewAccount()
        {
            Console.WriteLine("Please input the Deposit amount");
            string userInput = Console.ReadLine();
            
            if (decimal.TryParse(userInput, out decimal DepositAmount))
            {
                Account newAccount = new Account(DepositAmount);


                Accounts.Add(newAccount);

                Console.WriteLine("Account Created");
            }
            else
            {
                Console.WriteLine("Invalid Amount, Couldnt Create Account");
            }

        }
    }
}
