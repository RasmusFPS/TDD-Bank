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

        public static Dictionary<string, decimal> Currency = new()
        {
            {"EUR",0.09m },
            {"USD",  0.10m},
            {"DKK",  0.68m}
        };


        internal static List<TransferLog> TransferHistory = new List<TransferLog>();
        internal static List<Loan> ActiveLoans = new List<Loan>();

    }
}
