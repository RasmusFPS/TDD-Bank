using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Login
    {
        public List<Users> UserCollection = new List<Users>()
        {
            new Users(1, "Admin-Johan", "1234", true),
            new Users(2, "Carl", "Hawaa", false)

        }        




        internal void Add()
        {
            //-------------------------
            //Blueprint Usercreation
            //-------------------------

            //string name = Console.ReadLine(); 
            //string password = Console.ReadLine();
            //if (int.TryParse(Console.ReadLine(), out int id)) ;
            //new Users(id, name, password, isAdmin)
            

            
            foreach (var i in UserCollection)
            {
                Console.WriteLine(i);
            }
            //UserCollection.Add(id, Name, password);
        }
    }
}
