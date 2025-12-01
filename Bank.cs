using System;
using System.Collections.Generic;
using System.Linq;

namespace TDD_Bank
{
    internal class Bank
    {
        public int attempts = 0;
        private User _loggedin;

        public void Run()
        {
            bool Exit = true;
            while (Exit)
            {

                Exit = UI.WelcomeMSG();

                if (Exit == false)
                {
                    break;
                }

                SignIn();

                if (_loggedin == null) return;

                if (_loggedin is Client)
                {
                    RunClientDashboard();
                }
                else if (_loggedin is Admin)
                {
                    UI.AdminMenu();
                }

                _loggedin = null;
                Console.Clear();

            }
        }

        internal void SignIn()
        {
            attempts = 0;
            while (!Data.locked)
            {
                while (attempts < 3)
                {
                    string username = UI.GetUsername();
                    string userPassword = UI.GetPassword();
                    foreach (User user in Data.UserCollection)
                    {
                        if (user.Username.ToLower() == username && user.Password == userPassword)
                        {
                            Console.WriteLine($"Welcome {user.Username}");
                            _loggedin = user;
                            return;

                        }
                    }

                    attempts++;
                    Console.WriteLine("Wrong user-ID or password.");
                }

                if (attempts == 3)
                {
                    Data.locked = true;
                    Console.WriteLine("Too many attempts, ask admin for help.");
                    //_loggedin = null;


                }
            }
            if (Data.locked == true)
            {
                while (attempts != 6)
                {
                    string username = UI.GetUsername();
                    string userPassword = UI.GetPassword();
                    foreach (User user in Data.UserCollection)
                    {
                        
                        if (user.Username.ToLower() == username && user.Password == userPassword && user.IsAdmin == false)
                        {
                            Console.WriteLine($"{user.Username} you are locked out, ask Admin to unlock program");
                        }
                        else if (user.Username.ToLower() == username && user.Password == userPassword && user.IsAdmin == true)
                        {
                            Console.WriteLine($"Welcome {user.Username}");
                            _loggedin = user;
                            return;

                        }
                    }

                    attempts++;
                    Console.WriteLine("Wrong user-ID or password.");
                }
            }
        }

        private void RunClientDashboard()
        {
            Client currentclient = _loggedin as Client;

            if (currentclient == null) return;
            bool temp = true;
            while (temp)
            {
                string choice = UI.SignInMenu(currentclient);
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Show Accounts");
                        UI.ShowAccounts(currentclient);
                        break;
                    case "2":
                        TypeOfAccount(currentclient);
                        break;
                    case "3":
                        HandleDeposit(currentclient);
                        break;
                    case "4":
                        HandleWithdraw(currentclient);
                        break;
                    case "5":
                        BankTransfer.TransferToMe(currentclient);
                        //BankTransfer.TransferToOthers(currentclient);
                        break;
                    case "6":
                        return;
                }
            }
        }

        private void HandleDeposit(Client client)
        {
            UI.ShowAccounts(client);
            var accountNumber = UI.GetAccountNumber();
            var amount = UI.GetDecimal();
            Account account = client.GetAccount(accountNumber);

            if (account != null)
            {
                if (account.Deposit(amount))
                {
                    Console.WriteLine($"Deposit successful. New Balance{account.Balance}");
                }
                else
                {
                    Console.WriteLine("Deposit Failed");
                }
            }
            else
            {
                Console.WriteLine("Account Not Found");
            }
        }

        private void HandleWithdraw(Client client)
        {
            UI.ShowAccounts(client);
            var accountNumber = UI.GetAccountNumber();
            var amount = UI.GetDecimal();
            Account account = client.GetAccount(accountNumber);

            if (account != null)
            {
                if (account.Withdraw(amount))
                {
                    Console.WriteLine($"\nWithdrawal successful. New balance for account #{account.AccountNumber} is {account.Balance:C}.");
                }
                else
                {
                    Console.WriteLine("\nWithdrawal failed. Insufficient funds or invalid amount.");
                }
            }
            else
            {
                Console.WriteLine("Account not found");
            }
        }

        internal void TypeOfAccount(Client client)
        {
            Console.WriteLine("What type of Account do you want to make");
            Console.WriteLine("1.Bank Account\n2.Saving Account");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    client.CreateNewAccount();
                    break;
                case "2":
                    client.CreateSavingAccount();
                    break;
                default:
                    Console.WriteLine("Didnt choose Account correctly");
                    break;
            }
        }
    }
}


