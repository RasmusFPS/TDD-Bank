using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace TDD_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TDDBank bank = new TDDBank();
            User loggedInUser = TDDBank.SignIn();

            if (loggedInUser is Client currentClient)
            {
                RunClientDashboard(currentClient);
            }
            else if (loggedInUser is Admin admin)
            {
                UI.AdminMenu();

            }

            Console.WriteLine("Thank you for using TDD Bank");

            bank.Run();
        }
    }
}
