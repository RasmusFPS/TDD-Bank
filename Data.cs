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
        public static double _savingInterest;
        public static double _loanInterest;

        public static List<User> UserCollection = new List<User>()
        {
            new Admin( "Admin-Johan", "1234", true),
            new Client( "Carl", "Hawaa", false),
            new Client("Bob","123",false)

        };

        public static List<Exchange> Currency = new();


    }
}
