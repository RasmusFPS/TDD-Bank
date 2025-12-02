using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Admin : User
    {
        public Admin(string username, string password, bool isAdmin, int tries) : base(username, password, isAdmin, tries)
        {
        }

        internal void CreateNewUser()
        {
            string name = "Start";
            Console.Write("Insert name:");
            name = Console.ReadLine();

            List<string> Usernames = new List<string>();
            List<string> UsernamesLower = new List<string>();

            foreach (var i in Data.UserCollection)
            {
                Usernames.Add(i.Username);
                string username = i.Username.ToLower();
                UsernamesLower.Add(username);
            }

            if (!Usernames.Contains(name) && !UsernamesLower.Contains(name.ToLower()))
            {
                Console.Write("Insert password:");
                string password = Console.ReadLine();

                bool isAdmin = false;
                int tries = 3;

                Data.UserCollection.Add(new User(name, password, isAdmin, tries));

                foreach (var i in Data.UserCollection)
                {
                    Console.WriteLine($"New Client:\n {i.Username}, Password: {i.Password}\n");
                }

                Console.WriteLine("Press Enter To Continue...");
                Console.ReadKey();
                Console.Clear();
                UI.PrintedAdminMenu();
            }

            else
            {
                Console.WriteLine("Name already taken.");
            }



        }


        internal void UserLog()
        {
            Console.WriteLine("User Log:");
            foreach (var i in Data.UserCollection)
            {
                Console.WriteLine($"New Client:\n {i.Username}, Password: {i.Password}\n");
            }
            Console.WriteLine("Press Enter To Continue...");
            Console.ReadKey();
            Console.Clear();
            UI.PrintedAdminMenu();
        }
        internal void UserUnlock()
        {
            List<string> LockedUsers = new List<string>();
            foreach (var user in Data.UserCollection)
            {

                if (user.Tries == 0)
                {
                    //Console.WriteLine($"{i}. {user.Username}");
                    LockedUsers.Add(user.Username);
                }
            }
            if (LockedUsers.Count() != 0)
            {
                foreach (var i in LockedUsers)
                {
                    int nr = 1;
                    Console.WriteLine($" {nr}. {i}");
                }
                Console.Write("Lås upp:");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice < LockedUsers.Count + 1 && choice > 0)
                {

                    foreach (var i in Data.UserCollection)
                    {
                        if (i.Username == LockedUsers[choice - 1])
                        {
                            i.Tries = +3;
                        }
                    }
                    LockedUsers.RemoveAt(choice - 1);
                }
                else
                {
                    Console.WriteLine("Error, outside of list");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("No Locked users");
                Console.ReadKey();
            }
        }
    }
}
