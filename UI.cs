using System.Reflection.Metadata.Ecma335;

namespace TDD_Bank
{
    internal static class UI
    {
        internal static bool WelcomeMSG()
        {

            Console.WriteLine("Welcome to TDD Bank\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
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
            Console.ResetColor();

            Thread.Sleep(1000);
            Console.Clear();

            bool UserContinue = true;
            bool WaitingForInput = true;

            while (WaitingForInput)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Exit");
                Console.ResetColor();

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
                        UI.PrintMessage("Exiting application...");
                        Thread.Sleep(500);
                        UserContinue = false;
                        WaitingForInput = false;
                        break;
                    default:
                        ErrorMesage("INVALID");
                        Thread.Sleep(500);
                        Console.Clear();
                        break;

                }
            }
            return UserContinue;
        }

        internal static string GetUsername()
        {
            string username = null;
            bool correctUsername = false;
            while (!correctUsername)
            {
                Console.Write("User-ID: ");
                username = Console.ReadLine().ToLower();

                foreach (var user in Data.UserCollection)
                {
                    if (username == user.Username.ToLower())
                    {
                        correctUsername = true;
                    }
                }
                if (!correctUsername)
                {
                    ErrorMesage("Username Dosen't Exist.\nTry again.");
                    Thread.Sleep(800);
                    Console.Clear();
                }
                Console.Clear();

            }
            return username;
        }

        internal static string GetPassword()
        {
            bool emptyPassword = true;
            string userPassword = null;
            while (emptyPassword)
            {
                Console.Write("Password: ");
                userPassword = Console.ReadLine();
                if (userPassword.Trim() == "")
                {
                    ErrorMesage("Write something before clicking enter.");
                    Thread.Sleep(500);
                    Console.Clear();
                }
                else
                {
                    emptyPassword = false;
                }
            }
            Console.Clear();

            return userPassword;
        }

        public static string PrintedSignInMenu(Client client)
        {
            Console.ForegroundColor = ConsoleColor.White;
            PrintMessage("1. Show My Accounts");
            PrintMessage("2. Create New Account");
            PrintMessage("3. Deposit Money");
            PrintMessage("4. Withdraw Money");
            PrintMessage("5. Transfer to Me");
            PrintMessage("6. Transfer to Others");
            PrintMessage("7. Transferlog");
            PrintMessage("8. Loan");
            PrintMessage("9. Logout");
            Console.Write("Your choice: ");
            Console.ResetColor();

            return Console.ReadLine();
        }

        internal static void PrintedAdminMenu()
        {
            bool signedIn = true;
            while (signedIn)
            {
                PrintMessage("1. Update Currency");
                PrintMessage("2. Add Currency");
                PrintMessage("3. User Log");
                PrintMessage("4. Create New User");
                PrintMessage("5. Unlock Users");
                PrintMessage("6. Log Out");

                var input = Console.ReadLine();
                Admin admin = new Admin("", "", true, 3);


                switch (input)
                {
                    case "1":
                        PrintMessage("Update Currency");
                        CurrencyUpdate();
                        break;
                    case "2":
                        admin.AddCurrency();
                        break;
                    case "3":
                        admin.UserLog();
                        break;
                    case "4":
                        admin.CreateNewUser();
                        break;
                    case "5":
                        admin.UserUnlock();
                        break;
                    case "6":
                        signedIn = false;
                        break;
                }

            }
        }


        //prints the account balance
        internal static void ShowAccounts(Client client)
        {
            PrintMessage("Accounts:");

            if (!client.Accounts.Any())
            {
                ErrorMesage("You have no accounts.");
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
                    $"{Math.Round(account.Balance, 2)} {account.Currency}", // Combine balance and currency
                    account.AccountNumber,
                    accounttype));
            }
        }

        internal static int GetAccountNumber()
        {
            int AccountNumber;
            PrintMessage("Enter the Account Number: ");
            while (!int.TryParse(Console.ReadLine(), out AccountNumber))
            {
                ErrorMesage("Invalid Input.");
                PrintMessage("Enter Account Number: ");
            }

            return AccountNumber;
        }

        internal static decimal GetDecimal()
        {
            decimal amount;
            PrintMessage("Enter Amount: ");
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                ErrorMesage("Invaild Input. Try again.");
                PrintMessage("Enter Amount: ");
            }

            return amount;
        }

        internal static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        internal static void ErrorMesage(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(error);
            Console.ResetColor();
        }

        internal static void ShowTransfers()
        {
            Console.WriteLine("Transfers: ");

            foreach (var log in Data.TransferHistory)
            {
                Console.WriteLine($"{log.LogTime}: \n" +
                    $"{log.Amount} {log.Currency}, from account: {log.FromAccount} ({log.FromUser}) --> To account {log.ToAccount} ({log.ToUser})");
            }
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

                PrintMessage("Please choose a currency with index number: ");

                for (int i = 0; i < Data.Currency.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {listCurrency[i]}");
                }
                PrintMessage("Your Choice: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice > 0 && choice < listCurrency.Count + 1)
                    {
                        input = listCurrency[choice - 1];
                        validInput = true;
                    }
                    else
                    {
                        ErrorMesage("Invalid Number. Try Again.");
                        Thread.Sleep(600);
                    }
                }
                else
                {
                    ErrorMesage("Please Enter a Number: ");
                    Thread.Sleep(600);
                }
            }
            return input;
        }
        internal static void CurrencyUpdate()
        {
            Admin admin = new Admin("", "", true, 3);
            Console.WriteLine("Choose your action\n" +
                "1. Update Currency.\n" +
                "2. Add Currency.\n" +
                "3. Remove Currency.\n");
            int.TryParse(Console.ReadLine(), out int choice);
            switch (choice)
            {
                case 1:
                    admin.CurrencyUpdate();
                    break;
                case 2:
                    admin.AddCurrency();
                    break;
                case 3:
                    admin.CurrencyRemove();
                    break;
            }
        }

        internal static bool AskTryagain()
        {
            PrintMessage("Do you want to try again? y/n");
            string input = Console.ReadLine().ToLower();
            return input == "y" || input == "yes";
        }

    }
}
