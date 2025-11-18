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
            int id = UserCollection.Count + 1;
            bool isAdmin = false;
            UserCollection.Add(new User(id, name, password, isAdmin));





            foreach (var i in UserCollection)
            {
                Console.WriteLine($"New client\nID: {i.Id}, Client: {i.Username}, Password: {i.Password}\n");
            }
        }
    }
}
