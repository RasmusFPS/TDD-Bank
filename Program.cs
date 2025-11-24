using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace TDD_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //    //THIS IS ONLY TEMP TO TEST(we can put this in our Bank.cs Later to make the thing work) : )
            UI.WelcomeMSG();

            User loggedInUser = User.SignIn();

            if (loggedInUser is Client currentClient)
            {
                RunClientDashboard(currentClient);
            }
            else if (loggedInUser is Admin admin)
            {
                Console.WriteLine("Admin dashboard is not yet DONE");
            }

            Console.WriteLine("Thank you for using TDD Bank");

        }

        public static void RunClientDashboard(Client client)
        {
            bool temp = true;

            while (temp)
            {
                string choice = UI.SignInMenu(client);

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Show Accounts");
                        UI.ShowAccounts(client);
                        break;
                    case "2":
                        client.CreateNewAccount();
                        break;
                    case "3":
                        HandleDeposit(client);
                        break;
                    case "4":
                        HandleWithdraw(client);
                        break;
                    case "5":
                        Console.WriteLine("FANNYS METOD");
                        BankTransfer.TransferToMe(client);
                        break;
                }
            }

            
            

        }
        private static void HandleDeposit(Client client)
        {
            UI.ShowAccounts(client);

            var (accountNumber, amount) = UI.GetDeposit();

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

        private static void HandleWithdraw(Client client)
        {
            UI.ShowAccounts(client);

            var (accountNumber, amount) = UI.GetDeposit();

            Account account = client.GetAccount(accountNumber);

            if(account != null)
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
    }
}
