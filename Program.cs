using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace TDD_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //THIS IS ONLY TEMP TO TEST(we can put this in our Bank.cs Later to make the thing work) : )
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
                        UI.GetDeposit();
                        break;
                }
            }


        }
    }
}
