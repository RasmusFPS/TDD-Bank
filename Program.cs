using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace TDD_Bank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TDDBank bank = new TDDBank();

            bank.Run();
        }
    }
}
