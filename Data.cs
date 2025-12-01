using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    //Made Data Static to work in user class
    internal static class Data
    {
        public static decimal _loanInterest = 0.05m;

        public static List<User> UserCollection = new List<User>()
        {
            new Admin( "Admin-Johan", "1234", true),
            new Client( "Carl", "Hawaa", false),
            new Client("Bob","123",false)

        };
        //Dicitonary containing all currencies
        public static Dictionary<string, decimal> Currency = new()
        {
            {"SEK", 1 },
            {"EUR",0.09m },
            {"USD",  0.11m},
            {"DKK",  0.68m}
        };
        //Amount translated to SEK (Needed for Exchange.cs)
        public static decimal inSEK;
        public static bool locked = false;


        internal static List<TransferLog> TransferHistory = new List<TransferLog>();
        //internal static List<Loan> ActiveLoans = new List<Loan>();

    }
}
