namespace TDD_Bank
{
    internal class RunBank
    {
        public void WelcomeMSG()
        {
            Console.WriteLine("Welcome to TDD Bank\n");
            Console.WriteLine("___________________  ________    __________    _____    _______   ____  __.");
            Console.WriteLine("\\__    ___/\\______ \\ \\______ \\   \\______   \\  /  _  \\   \\      \\ |    |/ _|");
            Console.WriteLine("  |    |    |    |  \\ |    |  \\   |    |  _/ /  /_\\  \\  /   |   \\|      <  ");
            Console.WriteLine("  |    |    |    `   \\|    `   \\  |    |   \\/    |    \\/    |    \\    |  \\ ");
            Console.WriteLine("  |____|   /_______  /_______  /  |______  /\\____|__  /\\____|__  /____|__ \\");
            Console.WriteLine("                   \\/        \\/          \\/         \\/         \\/        \\/");

            Thread.Sleep(2000);
            Console.Clear();
        }

        public void ShowSignInMenu()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Going to Login");
                    break;
                case "2":
                    return;
            }
        }

        public void AdminMenu()
        {
            Console.WriteLine("1. Update Currency");
            Console.WriteLine("2. Create New account");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("Update Currency");
                    break;
                case "2":
                    return;
            }

        }

        public void NewAccount()
        {
            Console.WriteLine();
            Console.WriteLine("1. Bank Account");
            Console.WriteLine("2. Savings Account");
        }
    }
}
