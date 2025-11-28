namespace TDD_Bank
{
    internal static class UI
    {
        internal static void WelcomeMSG()
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

            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Going to Login");
                    Thread.Sleep(100);
                    Console.Clear();
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("INVALID");
                    break;

            }
        }

        internal static string GetUsername()
        {
            //Dubbelkolla felhantering
            Console.Write("User-ID:");
            string username = Console.ReadLine();

            Console.Clear();
            return username;
        }

        internal static string GetPassword()
        {
            //dubbelkolla felhantering
            Console.Write("Password:");
            string userPassword = Console.ReadLine();

            return userPassword;
        }

        public static string SignInMenu(Client client)
        {
            Console.WriteLine($"\n---- Welcome, {client.Username}! ----");
            Console.WriteLine("1. Show My Accounts");
            Console.WriteLine("2. Create New Account");
            Console.WriteLine("3. Deposit Money");
            Console.WriteLine("4. Withdraw Money");
            Console.WriteLine("5. Transfer Money");
            Console.WriteLine("6. Logout");
            Console.Write("Your choice: ");

            return Console.ReadLine();
        }

        internal static void AdminMenu()
        {
            Console.WriteLine("1. Update Currency");
            Console.WriteLine("2. User Log");
            Console.WriteLine("3. Create New User");
            Console.WriteLine("4. Log Out");

            var input = Console.ReadLine();
            Admin admin = new Admin("", "", true);

            switch (input)
            {
                case "1":
                    Console.WriteLine("Update Currency");
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

        internal static void NewAccount()
        {
            Console.WriteLine();
            Console.WriteLine("1. Bank Account");
            Console.WriteLine("2. Savings Account");
        }

        internal static decimal UserInput()
        {
            Console.Write("Please Enter the Amount:");
            var input = Convert.ToDecimal(Console.ReadLine());

            return input;
        }
        
        //prints the account balance
        internal static void ShowAccounts(Client client)
        {
            Console.WriteLine("Accounts");

            if (!client.Accounts.Any())
            {
                Console.WriteLine("You have no accounts");
                return;
            }

            Console.WriteLine("Balance      | Account Number | Type / Info");
            Console.WriteLine("-------------|----------------|--------------------");
            foreach (Account account in client.Accounts)
            {
                string accounttype = "Standard Account";

                if(account is SavingAccount)
                {
                    SavingAccount savingacc = (SavingAccount)account;

                    accounttype = $"Saving account {savingacc.IntrestRate} Intrest per year";
                }
                Console.WriteLine($"{account.Balance}{account.Currency,-8}  | {account.AccountNumber,-14}  | {accounttype}");
            }

        }

        internal static int GetAccountNumber()
        {
            Console.WriteLine("Enter the Account Number");
            int.TryParse(Console.ReadLine(), out int AccountNumber);

            return AccountNumber;
        }

        internal static decimal GetDecimal()
        {
            Console.WriteLine("Enter Amount:");
            decimal.TryParse(Console.ReadLine(), out decimal amount);

            return amount;
        }


        internal static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        internal static void AskQuestion(string question)
        {
            Console.WriteLine(question);
            question = Console.ReadLine();
        }

        internal static void PrintCurrency()
        {
        }
        
    }
}
