using System.Reflection.Metadata.Ecma335;

namespace TDD_Bank
{
    internal static class UI
    {
        internal static bool WelcomeMSG()
        {

            Console.WriteLine("Welcome to TDD Bank\n");
            Console.WriteLine("___________________  ________    __________    _____    _______   ____  __.");
            Thread.Sleep(200);
            Console.WriteLine("\\__    ___/\\______ \\ \\______ \\   \\______   \\  /  _  \\   \\      \\ |    |/ _|");
            Thread.Sleep(200);
            Console.WriteLine("  |    |    |    |  \\ |    |  \\   |    |  _/ /  /_\\  \\  /   |   \\|      <  ");
            Thread.Sleep(200);
            Console.WriteLine("  |    |    |    `   \\|    `   \\  |    |   \\/    |    \\/    |    \\    |  \\ ");
            Thread.Sleep(200);
            Console.WriteLine("  |____|   /_______  /_______  /  |______  /\\____|__  /\\____|__  /____|__ \\");
            Thread.Sleep(200);
            Console.WriteLine("                   \\/        \\/          \\/         \\/         \\/        \\/");

            Thread.Sleep(1000);
            Console.Clear();

            bool UserContinue = true;
            bool WaitingForInput = true;

            while (WaitingForInput)
            {
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Exit");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Thread.Sleep(100);
                        Console.Clear();
                        UserContinue = true;
                        WaitingForInput = false;
                        break;
                    case "2":
                        Console.WriteLine("Exiting application...");
                        Thread.Sleep(500);
                        UserContinue = false;
                        WaitingForInput = false;
                        break;
                    default:
                        Console.WriteLine("INVALID");
                        Console.WriteLine("Press 1 or 2");
                        Thread.Sleep(500);
                        Console.Clear();
                        break;

                }
            }
            return UserContinue;
        }

        internal static string GetUsername()
        {
            //Dubbelkolla felhantering, återanvändning av PrintMessage metoden?
            Console.Write("User-ID:");
            string username = Console.ReadLine();

            Console.Clear();
            return username.ToLower();
        }

        internal static string GetPassword()
        {
            //dubbelkolla felhantering, återanvändning av PrintMessage metoden?
            Console.Write("Password:");
            string userPassword = Console.ReadLine();

            return userPassword;
        }

        public static string PrintedSignInMenu(Client client)
        {
            PrintMessage("1. Show My Accounts");
            PrintMessage("2. Create New Account");
            PrintMessage("3. Deposit Money");
            PrintMessage("4. Withdraw Money");
            PrintMessage("5. Transfer Money");
            PrintMessage("6. Logout");
            Console.Write("Your choice: ");

            return Console.ReadLine();
        }

        internal static void PrintedAdminMenu()
        {
            PrintMessage("1. Update Currency");
            PrintMessage("2. User Log");
            PrintMessage("3. Create New User");
            PrintMessage("4. Log Out");

            var input = Console.ReadLine();
            Admin admin = new Admin("", "", true);

            switch (input)
            {
                case "1":
                    PrintMessage("Update Currency");
                    break;
                case "2":
                    admin.UserLog();
                    break;
                case "3":
                    admin.CreateNewUser();
                    return;
                case "4":

                    break;
            }

        }


        //prints the account balance
        internal static void ShowAccounts(Client client)
        {
            PrintMessage("Accounts");

            if (!client.Accounts.Any())
            {
                PrintMessage("You have no accounts");
                return;
            }

            PrintMessage(String.Format("{0,-15} | {1,-16} | {2,-35}", "Balance", "Account Number", "Type / Info"));
            PrintMessage("----------------|------------------|--------------------------------------");

            foreach (Account account in client.Accounts)
            {
                string accounttype = "Standard Account";

                if (account is SavingAccount)
                {
                    SavingAccount savingacc = (SavingAccount)account;
                    accounttype = $"Saving account ({savingacc.IntrestRate:P} interest)";
                }

                // This is the only line we changed. It forces each piece of data into a
                // column of a specific width, guaranteeing alignment.
                PrintMessage(String.Format("{0,-15} | {1,-16} | {2,-35}",
                    $"{account.Balance} {account.Currency}", // Combine balance and currency
                    account.AccountNumber,
                    accounttype));
            }
        }

        internal static int GetAccountNumber()
        {
            PrintMessage("Enter the Account Number");
            int.TryParse(Console.ReadLine(), out int AccountNumber);

            return AccountNumber;
        }

        internal static decimal GetDecimal()
        {
            decimal amount;
            PrintMessage("Enter Amount:");
            while (!decimal.TryParse(Console.ReadLine(), out amount)) ;
            {
                PrintMessage("Invaild Inupt. try again");
            }

            return amount;
        }


        internal static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        internal static void AskQuestion(string question)
        {
            PrintMessage(question);
            question = Console.ReadLine();
        }

        internal static void PrintCurrency()
        {
        }

        internal static string GetCurrency()
        {
            int choice;
            var listCurrency = Data.Currency.Keys.ToList();
            string input = null;
            bool validInput = false;
            while (!validInput)
            {
                Console.Clear();

                PrintMessage("Please choose a currency with index number:");

                for (int i = 0; i < listCurrency.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {listCurrency[i]}");
                }
                PrintMessage("Your choice:");

                

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if(choice > 0 &&  choice < listCurrency.Count)
                    {
                        input = listCurrency[choice-1];
                        validInput = true;
                    }
                    else
                    {
                        PrintMessage("Invalid number. Try again");
                        Thread.Sleep(400);
                    }
                }
                else
                {
                    PrintMessage("Please enter a number");
                    Thread.Sleep(1000);
                }
            }
            return input;
        }
        
    }
}
