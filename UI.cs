using System.Reflection.Metadata.Ecma335;

namespace TDD_Bank
{
    internal static class UI
    {
        internal static bool WelcomeMSG()
        {

            PrintMessage("Welcome to TDD Bank\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            PrintMessage("___________________  ________    __________    _____    _______   ____  __.");
            Thread.Sleep(200);
            PrintMessage("\\__    ___/\\______ \\ \\______ \\   \\______   \\  /  _  \\   \\      \\ |    |/ _|");
            Thread.Sleep(200);
            PrintMessage("  |    |    |    |  \\ |    |  \\   |    |  _/ /  /_\\  \\  /   |   \\|      <  ");
            Thread.Sleep(200);
            PrintMessage("  |    |    |    `   \\|    `   \\  |    |   \\/    |    \\/    |    \\    |  \\ ");
            Thread.Sleep(200);
            PrintMessage("  |____|   /_______  /_______  /  |______  /\\____|__  /\\____|__  /____|__ \\");
            Thread.Sleep(200);
            PrintMessage("                   \\/        \\/          \\/         \\/         \\/        \\/");
            Console.ResetColor();


            bool UserContinue = true;
            bool WaitingForInput = true;

            while (WaitingForInput)
            {
                Console.ForegroundColor = ConsoleColor.White;
                PrintMessage("1. Login");
                PrintMessage("2. Exit");
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
                        PrintMessage("Exiting Application...");
                        Thread.Sleep(500);
                        UserContinue = false;
                        WaitingForInput = false;
                        break;
                    default:
                        ErrorMessage("Invalid Input...");
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
                    ErrorMessage("Username Dosen't Exist.\nTry again.");
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
                    ErrorMessage("Write Something Before Pressing Enter.");
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
            PrintMessage("5. Internal Transfer");
            PrintMessage("6. Transfer to Others");
            PrintMessage("7. Transferlog");
            PrintMessage("8. Loan");
            PrintMessage("9. Logout");
            Console.Write("Your Choice: ");
            Console.ResetColor();

            return Console.ReadLine();


        }

        internal static void PrintedAdminMenu()
        {
            bool signedIn = true;
            while (signedIn)
            {
                Console.Clear();

                PrintMessage("1. Edit Currencies");
                PrintMessage("2. User Log");
                PrintMessage("3. Create New User");
                PrintMessage("4. Unlock Users");
                PrintMessage("5. Log Out");
                Console.Write("Your Choice: ");
                int input;
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    Console.Clear();

                    Admin admin = new Admin("", "", true, 3);
                    if (input <= 5 && input >= 1)
                    {
                        switch (input)
                        {
                            case 1:
                                CurrencyEdit();
                                break;
                            case 2:
                                admin.UserLog();
                                break;
                            case 3:
                                admin.CreateNewUser();
                                break;
                            case 4:
                                admin.UserUnlock();
                                break;
                            case 5:
                                signedIn = false;
                                break;
                        }
                    }
                    else
                    {
                        ErrorMessage("Wrong Input, Please Choose a Valid Option.");
                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    ErrorMessage("Wrong Input, Please Choose a Valid Option");
                    Thread.Sleep(2000);
                }
            }
        }


        //prints the account balance
        internal static void ShowAccounts(Client client)
        {
            PrintMessage("Accounts:");

            if (!client.Accounts.Any())
            {
                ErrorMessage("You Have No Accounts.");
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
                    accounttype = $"Saving Account (2% Interest)";
                }

                // This is the only line we changed. It forces each piece of data into a
                // column of a specific width, guaranteeing alignment.
                PrintMessage(String.Format("{0,-15} | {1,-16} | {2,-35}",
                    $"{Math.Round(account.Balance, 2)} {account.Currency}", // Combine balance and currency
                    account.AccountNumber,
                    accounttype));
            }
            UI.PrintMessage("");
        }

        internal static int GetAccountNumber()
        {
            int AccountNumber;
            PrintMessage("Enter the Account Number: ");
            while (!int.TryParse(Console.ReadLine(), out AccountNumber))
            {
                ErrorMessage("Invalid Input.");
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
                ErrorMessage("Invaild Input. Try again.");
                PrintMessage("Enter Amount: ");
            }

            return amount;
        }

        internal static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        internal static void ErrorMessage(string error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintMessage(error);
            Console.ResetColor();
        }

        internal static void SuccessMessage(string success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(success);
            Console.ResetColor();
        }


        internal static void ShowTransfers(Client client)
        {
            PrintMessage("Transfers: ");
            if (client.TransferHistory.Count == 0)
            {
                ErrorMessage("You Have No Transactions to Show.");
            }

            foreach (var log in client.TransferHistory)
            {
                PrintMessage($"{log.LogTime}: \n" +
                    $"{log.Amount} {log.Currency}, From Account Number: {log.FromAccount} --> To Account Number {log.ToAccount}");
            }
            PrintMessage("Press Any Key to Return to Menu...");
            Console.ReadKey();
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

                PrintMessage("Please Choose a Currency With Index Number: ");

                for (int i = 0; i < Data.Currency.Count; i++)
                {
                    PrintMessage($"{i + 1}. {listCurrency[i]}");
                }
                Console.Write("Your Choice: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice > 0 && choice < listCurrency.Count + 1)
                    {
                        input = listCurrency[choice - 1];
                        validInput = true;
                    }
                    else
                    {
                        ErrorMessage("Invalid Number. Try Again.");
                        Thread.Sleep(600);
                    }
                }
                else
                {
                    ErrorMessage("Please Enter a Number: ");
                    Thread.Sleep(600);
                }
            }
            return input;
        }
        internal static void CurrencyEdit()
        {
            int choice = 0;

            while (choice != 5)
            {
                    Admin admin = new Admin("", "", true, 3);
                PrintMessage("Choose Your Action. \n" +
                    "1. View Currencies. \n" +
                    "2. Update Currency.\n" +
                    "3. Add Currency.\n" +
                    "4. Remove Currency.\n" +
                    "5. Go Back.");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case 1:
                        foreach (var i in Data.Currency)
                        {
                            PrintMessage($"{i.Key} | {i.Value}");
                        }
                        PrintMessage("Press Any Key to Return to Menu...");
                        Console.ReadLine();
                        break;
                    case 2:
                        admin.CurrencyUpdate();
                        break;
                    case 3:
                        admin.AddCurrency();
                        break;
                    case 4:
                        admin.CurrencyRemove();
                        break;
                    case 5:
                        break;
                    default:
                        ErrorMessage("Please Choose a Correct Option.");
                        break;
                }
                
            }
        }

        internal static bool AskTryagain()
        {
            PrintMessage("Enter y to Try Again or Press Any Key to Return to menu.");
            string input = Console.ReadLine().ToLower();
            return input == "y" || input == "yes";
        }

    }
}
