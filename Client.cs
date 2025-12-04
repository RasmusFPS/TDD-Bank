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
        //internal List<Loan> Loans { get; private set; }
        internal List<Account> Accounts { get; private set; }
        public bool IsLocked { get; set; }
        public Client(string username, string password, bool isAdmin, int tries, bool isLocked) : base(username, password, false, tries)
        {
            //new makes sure the account is fresh and created for the object
            Accounts = new List<Account>();
            IsLocked = isLocked;
            //Loans = new List<Loan>();
        }

        public Account GetAccount(int accountNumber)
        {
            foreach (var account in Accounts)
            {
                if(account.AccountNumber == accountNumber)
                {
                    return account;
                }
            }

            return null;
        }
        
        //This takes user input on how much money should be in the account
        public void CreateNewAccount()
        {
            UI.PrintMessage("Please input the Deposit amount");
            string userInput = Console.ReadLine();
            UI.PrintMessage("Choose Currency:");
            string input = UI.GetCurrency();

            if (decimal.TryParse(userInput, out decimal DepositAmount))
            {
                Account newAccount = new Account(DepositAmount,input);

                Accounts.Add(newAccount);

                UI.PrintMessage("Account Created");
            }
            else
            {
                UI.PrintMessage("Invalid Amount, Couldnt Create Account");
            }
        }

        public void CreateSavingAccount()
        {
            Console.WriteLine("Please input deposit amount");
            string userInput = Console.ReadLine();
            UI.PrintMessage("Choose currency");
            foreach (var i in Data.Currency)
            {
                Console.WriteLine(i);
            }
            var input = Console.ReadLine().ToUpper();
            UI.PrintMessage("The intrest Rate is 2% per year");
            if(decimal.TryParse(userInput,out decimal DepositAmount))
            {
                SavingAccount newAccount = new SavingAccount(DepositAmount,input,0.02m);

                Accounts.Add(newAccount);

                UI.PrintMessage("Saving Account created");
            }
            else
            {
                UI.PrintMessage("Invalid Amount, Couldnt create account");
            }
        }
    }
}
