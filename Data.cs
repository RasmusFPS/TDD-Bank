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
        public static DateTime Runtime;
        public static decimal _loanInterest = 0.05m;

        public static Client anna = new Client("Anna", "123", false, 3, false);
        public static Client bob = new Client("Bob", "123", false, 3, false);
        public static Client louise = new Client("Louise","123",false,3,false);

        static Data()
        {
            Runtime = DateTime.Now.AddMinutes(1);
            anna.Accounts.Add(new Account(500, "SEK"));
            anna.Accounts.Add(new SavingAccount(500, "SEK", 0.02m));

            bob.Accounts.Add(new Account(500, "SEK"));
            bob.Accounts.Add(new SavingAccount(500, "SEK", 0.02m));

            louise.Accounts.Add(new Account(100, "SEK"));
        }
        
        public static List<User> UserCollection = new List<User>()
        {
            new Admin( "Admin-Fanny", "1234", true, 3),
            bob,
            anna,
            louise
        };
        
        //Dicitonary containing all currencies
        public static Dictionary<string, decimal> Currency = new()
        {
            {"SEK", 1 },
            {"EUR", 0.09m },
            {"USD",  0.11m},
            {"DKK",  0.68m}
        };

        internal static List<Loan> ActiveLoans = new List<Loan>();
        public static List<PendingTrans> TransferQueue = new List<PendingTrans>();
    }
}
