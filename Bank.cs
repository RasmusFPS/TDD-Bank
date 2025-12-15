using System;
using System.Collections.Generic;
using System.Linq;

namespace TDD_Bank
{
    internal class Bank
    {
        internal int attempts = 0;
        private User _loggedin;

        internal void Run()
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
                        UI.ErrorMessage("Login Failed. Returning to Main Menu.");
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
                string? userPassword = UI.GetPassword();
                foreach (User user in Data.UserCollection)
                {
                    if (user.Tries == 0)
                    {
                        UI.ErrorMessage("Locked User, Ask Admin For Help.");
                    }
                    if (user.Username.ToLower() == username && user.Password == userPassword && user.Tries > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        UI.PrintMessage($"Welcome {user.Username}");
                        Console.ResetColor();
                        _loggedin = user;
                        signedIn = true;
                        user.Tries = 3;
                        return;
                    }
                    else if (user.Username.ToLower() == username && user.Password != userPassword && user.Tries > 0)
                    {
                        UI.ErrorMessage("Wrong User-ID or Password.");

                        if (!user.IsAdmin)
                        {
                            user.Tries--;
                        }
                        if (user.Tries == 0)
                        {
                            UI.ErrorMessage("Locked User, Ask Admin For Help.");
                            Console.ReadKey();
                            break;
                        }

                        Thread.Sleep(1500);
                        Console.Clear();
                    }

                }
            }
        }

        private void RunClientDashboard()
        {
            Client? currentclient = _loggedin as Client;

            if (currentclient == null) return;
            bool temp = true;
            while (temp)
            {
                BankTransfer.CheckQueue();
                Console.Clear();

                string? choice = UI.PrintedSignInMenu(currentclient);
                Console.Clear();
                switch (choice)
                {
                    case "1":
                        UI.PrintMessage("Show Accounts");
                        UI.ShowAccounts(currentclient);
                        UI.PrintMessage("Press Any Key to Return to Menu...");
                        Console.ReadKey();
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
                        UI.ShowTransfers(currentclient);
                        break;
                    case "8":
                        Loan.ApplyForLoan(currentclient);
                        break;
                    case "9":
                        return;
                    default:
                        UI.ErrorMessage("Invalid Input.");
                        UI.PrintMessage("Press Any Key to Return to Menu...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        private void HandleDeposit(Client client)
        {
            bool keepTrying = true;
            while (keepTrying)
            {

                UI.ShowAccounts(client);
                if(client.Accounts.Count == 0)
                {
                    Console.Clear();
                    UI.ErrorMessage("You Have no Accounts, Can't Use This Function.");
                    Thread.Sleep(3000);
                    keepTrying = false;
                    break;
                }
                var accountNumber = UI.GetAccountNumber();
                var amount = UI.GetDecimal();
                Account? account = client.GetAccount(accountNumber);

                if (account != null)
                {
                    if (account.Deposit(amount))
                    {
                        UI.SuccessMessage($"Deposit Successful. New Balance For Account {account.AccountNumber} is {account.Balance} {account.Currency}.");
                        keepTrying = false;
                        UI.PrintMessage("Press Any Key to Return to Menu...");
                        Console.ReadKey();

                    }
                    else
                    {
                        UI.ErrorMessage("Deposit Failed.");
                        keepTrying = UI.AskTryagain();
                        Console.Clear();
                    }
                }
                else
                {
                    UI.ErrorMessage("Account Not Found.");
                    keepTrying = UI.AskTryagain();
                    Console.Clear();
                }
            }
        }

        private void HandleWithdraw(Client client)
        {
            bool keepTrying = true;
            while (keepTrying)
            {

                UI.ShowAccounts(client);
                
                if (client.Accounts.Count == 0)
                {
                    Console.Clear();
                    UI.ErrorMessage("You Have no Accounts, Can't Use This Function.");
                    Thread.Sleep(3000);
                    keepTrying = false;
                    break;
                }

                var accountNumber = UI.GetAccountNumber();
                var amount = UI.GetDecimal();
                Account? account = client.GetAccount(accountNumber);

                if (account != null)
                {
                    if (account.Withdraw(amount))
                    {
                        UI.SuccessMessage($"\nWithdrawal Successful. New Balance For Account {account.AccountNumber} is {account.Balance} {account.Currency}.");
                        keepTrying = false;
                        UI.PrintMessage("Press Any Key to Return to Menu...");
                        Console.ReadKey();
                    }
                    else
                    {
                        UI.ErrorMessage("\nWithdrawal Failed. Insufficient Funds or Invalid Amount.");
                        keepTrying = UI.AskTryagain();
                        Console.Clear();
                    }
                }
                else
                {
                    UI.ErrorMessage("Account Not Found.");
                    keepTrying = UI.AskTryagain();
                    Console.Clear();
                }
            }
        }

        internal void TypeOfAccount(Client client)
        {
            bool tryAgain = true;
            while (tryAgain)
            {
                UI.PrintMessage("What Type of Account do You Want to Open?");
                UI.PrintMessage("1.Bank Account\n2.Saving Account");
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        client.CreateNewAccount();
                        tryAgain = false;
                        break;
                    case "2":
                        client.CreateSavingAccount();
                        tryAgain= false;
                        break;
                    default:
                        UI.ErrorMessage("Invalid Input.");
                        tryAgain = UI.AskTryagain();
                        break;
                }
            }
        }
    }
}


