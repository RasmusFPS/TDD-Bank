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

                if(Exit == false)
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
                    UI.PrintedAdminMenu();
                }

                _loggedin = null;
                Console.Clear();

            }
        }

        internal void SignIn()
        {
            attempts = 0;

            {
                while (attempts < 3)
                {
                    string username = UI.GetUsername();
                    string userPassword = UI.GetPassword();

                    foreach (User user in Data.UserCollection)
                    {
                        if (user.Username.ToLower() == username && user.Password == userPassword)
                        {
                            UI.PrintMessage($"Welcome {user.Username}");
                            _loggedin = user;
                            return;

                        }
                    }

                    attempts++;
                    UI.PrintMessage("Wrong user-ID or password.");
                }

                if (attempts == 3)
                {
                    UI.PrintMessage("Too many attempts.");
                    _loggedin = null;
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
                string choice = UI.PrintedSignInMenu(currentclient);
                switch (choice)
                {
                    case "1":
                        UI.PrintMessage("Show Accounts");
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
                    UI.PrintMessage($"Deposit successful. New Balance{account.Balance}");
                }
                else
                {
                    UI.PrintMessage("Deposit Failed");
                }
            }
            else
            {
                UI.PrintMessage("Account Not Found");
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
                    UI.PrintMessage($"\nWithdrawal successful. New balance for account #{account.AccountNumber} is {account.Balance:C}.");
                }
                else
                {
                    UI.PrintMessage("\nWithdrawal failed. Insufficient funds or invalid amount.");
                }
            }
            else
            {
                UI.PrintMessage("Account not found");
            }
        }

        internal void TypeOfAccount(Client client)
        {
            UI.PrintMessage("What type of Account do you want to make");
            UI.PrintMessage("1.Bank Account\n2.Saving Account");
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
                    UI.PrintMessage("Didnt choose Account correctly");
                    break;
            }
        }
    }
}


