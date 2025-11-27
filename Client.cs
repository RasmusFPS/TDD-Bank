using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Client : User
    {
        internal List<Loan> Loans { get; private set; }
        internal List<Account> Accounts { get; private set; }
        public Client(string username, string password, bool isAdmin) : base(username, password, false)
        {
            //new makes sure the account is fresh and created for the object
            Accounts = new List<Account>();
            Loans = new List<Loan>();
        }

        public Account GetAccount(int accountNumber)
        {
            foreach (var account in Accounts)
            {
                return account;
            }

            return null;
        }
        
        //This takes user input on how much money should be in the account
        public void CreateNewAccount()
        {
            Console.WriteLine("Please input the Deposit amount");
            string userInput = Console.ReadLine();
            Console.WriteLine("Choose Currency:");
            foreach (var i in Data.Currency)
            {
                Console.WriteLine(i);
            }
            string input = Console.ReadLine().ToUpper();
            
            if (decimal.TryParse(userInput, out decimal DepositAmount))
            {
                Account newAccount = new Account(DepositAmount,input);


                Accounts.Add(newAccount);

                Console.WriteLine("Account Created");
            }
            else
            {
                Console.WriteLine("Invalid Amount, Couldnt Create Account");
            }

        }

        public void CreateSavingAccount()
        {
            Console.WriteLine("Please input deposit amount");
            string userInput = Console.ReadLine();
            Console.WriteLine("Choose currency");
            foreach (var i in Data.Currency)
            {
                Console.WriteLine(i);
            }
            var input = Console.ReadLine().ToUpper();
            Console.WriteLine("The intrest Rate is 2% per year");
            if(decimal.TryParse(userInput,out decimal DepositAmount))
            {
                SavingAccount newAccount = new SavingAccount(DepositAmount,input,0.02m);

                Accounts.Add(newAccount);

                Console.WriteLine("Saving Account created");
            }
            else
            {
                Console.WriteLine("Invalid Amount, Couldnt create account");
            }
        }
    }
}
