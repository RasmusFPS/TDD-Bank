using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Admin : User
    {
        public Admin(string username, string password, bool isAdmin) : base(username, password, isAdmin)
        {
        }

        internal void CreateNewUser()
        {
            //-------------------------
            //Blueprint Usercreation
            //-------------------------

            Console.Write("Insert name:");
            string name = Console.ReadLine();
            Console.Write("Insert password:");
            string password = Console.ReadLine();
            bool isAdmin = false;
            UserCollection.Add(new User(name, password, isAdmin));





            foreach (var i in UserCollection)
            {
                Console.WriteLine($"New Client:\n {i.Username}, Password: {i.Password}\n");
            }
        }
    }
}
