namespace TDD_Bank
{
    internal class RunBank
    {
        public void WelcomeMSG()
        {
            Console.WriteLine("Welcome to TDD Bank\n");
            Console.WriteLine("___________________  ________    __________    _____    _______   ____  __.");
            Thread.Sleep(400);
            Console.WriteLine("\\__    ___/\\______ \\ \\______ \\   \\______   \\  /  _  \\   \\      \\ |    |/ _|");
            Thread.Sleep(400);
            Console.WriteLine("  |    |    |    |  \\ |    |  \\   |    |  _/ /  /_\\  \\  /   |   \\|      <  ");
            Thread.Sleep(400);
            Console.WriteLine("  |    |    |    `   \\|    `   \\  |    |   \\/    |    \\/    |    \\    |  \\ ");
            Thread.Sleep(400);
            Console.WriteLine("  |____|   /_______  /_______  /  |______  /\\____|__  /\\____|__  /____|__ \\");
            Thread.Sleep(400);
            Console.WriteLine("                   \\/        \\/          \\/         \\/         \\/        \\/");

            Thread.Sleep(2000);
            Console.Clear();

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

        public void SignInMenu()
        {
            Console.WriteLine("----Welcome To your Bank----");
            Console.WriteLine("1. Show your accounts Balance");
            Console.WriteLine("2. ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("");
                    break;
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
