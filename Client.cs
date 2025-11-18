using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Client : User
    {
        public Client(string username, string password, bool isAdmin) : base(username, password, isAdmin)
        {
        }
    }
}
