using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace TDD_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //    RunBank runbank = new RunBank();

            //    runbank.WelcomeMSG();

            UI.WelcomeMSG();
            User.SignIn();



        }
    }
}
