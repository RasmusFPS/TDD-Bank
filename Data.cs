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
        internal static DateTime Runtime;
        internal static decimal _loanInterest = 0.05m;

        internal static Client carl = new Client("Carl", "Hawaa", false, 3, false);
        internal static Client bob = new Client("Bob", "123", false, 3, false);
        internal static Client rasmu = new Client("Rasmu","123",false,3,false);

        static Data()
        {
            Runtime = DateTime.Now.AddMinutes(1);
            carl.Accounts.Add(new Account(500, "SEK"));
            carl.Accounts.Add(new SavingAccount(500, "SEK", 0.02m));

            bob.Accounts.Add(new Account(500, "SEK"));
            bob.Accounts.Add(new SavingAccount(500, "SEK", 0.02m));
        }

        internal static List<User> UserCollection = new List<User>()
        {
            new Admin( "Admin-Johan", "1234", true, 3),
            bob,
            carl,
            rasmu
        };

        //Dicitonary containing all currencies
        internal static Dictionary<string, decimal> Currency = new()
        {
            {"SEK", 1 },
            {"EUR", 0.09m },
            {"USD",  0.11m},
            {"DKK",  0.68m}
        };

        internal static List<Loan> ActiveLoans = new List<Loan>();
        internal static List<PendingTrans> TransferQueue = new List<PendingTrans>();
    }
}
