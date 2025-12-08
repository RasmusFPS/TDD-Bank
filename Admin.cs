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
                bool isLocked = false;

                Data.UserCollection.Add(new Client(name, password, isAdmin, tries, false));

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
                UI.ErrorMessage("Name already taken.");
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
                    UI.ErrorMessage("Error, outside of list");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("No Locked users");
                Console.ReadKey();
            }
        }
        internal void AddCurrency()
        {
            bool isNumber = false;
            Console.WriteLine("What currency would you like to add?");
            string currency = Console.ReadLine().ToUpper();
            foreach (char i in currency)
            {
                if (char.IsNumber(i))
                {
                    isNumber = true;
                    UI.ErrorMessage("Can't contain numbers.");
                }
            }
            if (!Data.Currency.ContainsKey(currency) && currency.Length == 3 && !isNumber)
            {

                Console.WriteLine("What is the current exchangerate to SEK from this currency");
                if (decimal.TryParse(Console.ReadLine(), out decimal exchange) && exchange > 0)
                {
                    Data.Currency.Add(currency, exchange);
                }
                else
                {
                    UI.ErrorMessage("Wrong input");
                }

            }
            else if (Data.Currency.ContainsKey(currency))
            {
                UI.ErrorMessage("This currency already exists.");
            }
            else
            {
                UI.ErrorMessage("Wrong input");
            }
        }
        internal void CurrencyUpdate()
        {

            Console.WriteLine("What currency do you want to edit?");
            foreach (var i in Data.Currency)
            {
                Console.WriteLine($"{i.Key} | {i.Value}");
            }

            

            string choice = Console.ReadLine().ToUpper();
            if (Data.Currency.ContainsKey(choice))
            {
                Console.WriteLine("How many percent?");
                decimal.TryParse(Console.ReadLine(), out decimal percent);
                Data.Currency[choice] *= (1 + percent / 100);
                Console.ReadKey();
            }
            else if (!Data.Currency.ContainsKey(choice))
            {
                UI.ErrorMessage("Fel Input");
            }
            else if (!Data.Currency.ContainsKey(choice))
            {
                UI.ErrorMessage("Wrong Input");
            }
        }
        internal void CurrencyRemove()
        {
            Console.WriteLine("Choose the Currency you want to remove:");
            foreach(var i in Data.Currency)
            {
                Console.WriteLine(i.Key);
            }
            string choice = Console.ReadLine().ToUpper();
            if (Data.Currency.ContainsKey(choice))
            {
                Console.WriteLine($"Are you sure you wanna remove {choice}? y/n");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    Data.Currency.Remove(choice);
                }
                else
                {
                    Console.WriteLine("Exiting, press enter to continue");
                    Thread.Sleep(500);
                }
            }
        }
    }
}

