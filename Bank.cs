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
            bool exit = false;
            while (!exit)
            {
                bool userWantsToContinue = UI.WelcomeMSG();

                if (userWantsToContinue)
                {
                    SignIn();

                    if (_loggedin == null)
                    {
                        UI.ErrorMesage("Login failed. Returning to main menu.");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                    else
                    {
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
                else
                {
                    exit = true;
                }
            }
        }
        internal void SignIn()
        {
            bool signedIn = false;
            while (!signedIn)
            {
                string username = UI.GetUsername();
                string userPassword = UI.GetPassword();
                foreach (User user in Data.UserCollection)
                {
                    if (user.Tries == 0)
                    {
                        Console.WriteLine("Locked user, ask admin for help");
                    }
                    if (user.Username.ToLower() == username && user.Password == userPassword && user.Tries > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        UI.PrintMessage($"Welcome {user.Username}");
                        Console.ResetColor();
                        _loggedin = user;
                        signedIn = true;
                        return;
                    }
                    else if (user.Username.ToLower() == username && user.Password != userPassword && user.Tries > 0)
                    {
                        UI.ErrorMesage("Wrong user-ID or password.");
                        user.Tries--;
                    }

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
                BankTransfer.CheckQueue();

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
                        break;
                    case "6":
                        BankTransfer.TransferToOthers(currentclient);
                        break;
                    case "7":
                        UI.ShowTransfers();
                        break;
                    case "8":
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
                    UI.PrintMessage($"Deposit successful. New Balance {account.Balance} {account.Currency}");
                }
                else
                {
                    UI.ErrorMesage("Deposit Failed");
                }
            }
            else
            {
                UI.ErrorMesage("Account Not Found");
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
                    UI.PrintMessage($"\nWithdrawal successful. New balance for account #{account.AccountNumber} is {account.Balance} {account.Currency}.");
                }
                else
                {
                    UI.ErrorMesage("\nWithdrawal failed. Insufficient funds or invalid amount.");
                }
            }
            else
            {
                UI.ErrorMesage("Account not found");
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
                    UI.ErrorMesage("Didnt choose Account correctly");
                    break;
            }
        }
    }
}


