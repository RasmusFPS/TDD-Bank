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

        public static Client carl = new Client("Carl", "Hawaa", false, 3, false);
        public static Client bob = new Client("Bob", "123", false, 3, false);

        static Data()
        {
            carl.Accounts.Add(new Account(0, "SEK"));
            carl.Accounts.Add(new SavingAccount(0, "SEK", 0.02m));

            bob.Accounts.Add(new Account(0, "SEK"));
            bob.Accounts.Add(new SavingAccount(0, "SEK", 0.02m));
        }


        public static List<User> UserCollection = new List<User>()
        {
            new Admin( "Admin-Johan", "1234", true, 3),
            bob,
            carl

        };
        

        //Dicitonary containing all currencies
        public static Dictionary<string, decimal> Currency = new()
        {
            {"SEK", 1 },
            {"EUR", 0.09m },
            {"USD",  0.11m},
            {"DKK",  0.68m}
        };

        //Amount translated to SEK (Needed for Exchange.cs)
        public static bool locked = false;


        internal static List<TransferLog> TransferHistory = new List<TransferLog>();
        //internal static List<Loan> ActiveLoans = new List<Loan>();

    }
}
