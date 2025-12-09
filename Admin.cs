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
            Console.Write("Insert name: ");
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
                Console.Write("Insert password: ");
                string password = Console.ReadLine();

                bool isAdmin = false;
                int tries = 3;
                bool isLocked = false;

                Data.UserCollection.Add(new Client(name, password, isAdmin, tries, false));

                

                foreach (var i in Data.UserCollection)
                {
                    string pas = new string('*', i.Password.Length);

                    Console.WriteLine($"New Client:\n {i.Username}, Password: {pas}\n");
                }

                UI.PrintMessage("Press Enter to Continue...");
                Console.ReadKey();
                Console.Clear();
            }

            else
            {
                UI.ErrorMessage("Name already taken.");
                Thread.Sleep(600);
            }

        }


        internal void UserLog()
        {
            Console.WriteLine("User Log: ");

            foreach (var i in Data.UserCollection)
            {
                string pas = new string('*', i.Password.Length);
                Console.WriteLine($"New Client:\n {i.Username}, Password: {pas}\n");
            }
            UI.PrintMessage("Press Enter To Continue...");
            Console.ReadKey();
            Console.Clear();
        }

        internal void UserUnlock()
        {
            List<string> LockedUsers = new List<string>();
            foreach (var user in Data.UserCollection)
            {

                if (user.Tries == 0)
                {
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
                Console.Write("Lock up: ");
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
                    UI.ErrorMessage("Error, Outside of List.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("No Locked Users.");
                Console.ReadKey();
            }
        }
        internal void AddCurrency()
        {
            bool isNumber = false;
            Console.WriteLine("What Currency Would You Like to Add? ");
            string currency = Console.ReadLine().ToUpper();
            foreach (char i in currency)
            {
                if (char.IsNumber(i))
                {
                    isNumber = true;
                    UI.ErrorMessage("Can't Contain Numbers.");
                }
            }
            if (!Data.Currency.ContainsKey(currency) && currency.Length == 3 && !isNumber)
            {

                Console.WriteLine("What's the Current Exchangerate to SEK From This Currency? ");
                if (decimal.TryParse(Console.ReadLine(), out decimal exchange) && exchange > 0)
                {
                    Data.Currency.Add(currency, exchange);
                    Console.WriteLine($"{currency} was added");
                    Thread.Sleep(600);
                }
                else
                {
                    UI.ErrorMessage("Wrong input.");
                    Thread.Sleep(1200);
                }

            }
            else if (Data.Currency.ContainsKey(currency))
            {
                UI.ErrorMessage("This currency already exists.");
                Thread.Sleep(1200);
            }
            else
            {
                UI.ErrorMessage("Wrong input, must be a valid ISO currency-code.");
                Thread.Sleep(1200);
            }
        }
        internal void CurrencyUpdate()
        {

            Console.WriteLine("Which Currency Do you Want to Edit?");
            foreach (var i in Data.Currency)
            {
                Console.WriteLine($"{i.Key} | {i.Value}");
            }



            string choice = Console.ReadLine().ToUpper();
            if (Data.Currency.ContainsKey(choice))
            {
                Console.WriteLine("How Many Percent?");
                decimal.TryParse(Console.ReadLine(), out decimal percent);
                Data.Currency[choice] *= (1 + percent / 100);
                Console.WriteLine($"{choice} was raised with {percent}%");
                Thread.Sleep(1200);
            }
            else if (!Data.Currency.ContainsKey(choice))
            {
                UI.ErrorMessage("Wrong Input, must be a valid ISO currency code.");
                Thread.Sleep(1200);
            }
            
        }
        internal void CurrencyRemove()
        {
            Console.WriteLine("Choose the Currency You Want to Remove: ");
            foreach(var i in Data.Currency)
            {
                Console.WriteLine(i.Key);
            }
            string choice = Console.ReadLine().ToUpper();
            if (Data.Currency.ContainsKey(choice))
            {
                Console.WriteLine($"Are You Sure You Want To Remove {choice}? y/n ");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    Data.Currency.Remove(choice);
                    Console.WriteLine($"{choice} was successfully removed from our currencies");
                    Thread.Sleep(600);
                }
                else
                {
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(600);
                }
            }
            else
            {
                UI.ErrorMessage($"{choice} does not match any currency supported by TDD");
                Thread.Sleep(600);
            }
        }
    }
}

