using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Data
    {
        public static List<User> UserCollection = new List<User>()
        {
            new User( "Admin-Johan", "1234", true),
            new User( "Carl", "Hawaa", false)

        };

        public static List<Exchange> Currency = new();
    }
}
