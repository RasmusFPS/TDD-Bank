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
            Console.Write("Insert Name: ");
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
                Console.Write("Insert Password: ");
                string password = Console.ReadLine();
                if (password.Length > 3)
                {

                    bool isAdmin = false;
                    int tries = 3;
                    bool isLocked = false;

                    Data.UserCollection.Add(new Client(name, password, isAdmin, tries, false));
                }
                else
                {
                    UI.ErrorMessage("Password Must Be Atleast 3 Characters");
                    Thread.Sleep(600);
                }
                foreach (var i in Data.UserCollection)
                {
                    string pas = new string('*', i.Password.Length);

                        Console.WriteLine($"User:\n {i.Username}, Password: {pas}\n");
                    }

                UI.PrintMessage("Press Any Key to Return to Menu...");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                UI.ErrorMessage("Name Already Taken.");
                Thread.Sleep(600);
            }

        }


        internal void UserLog()
        {
            Console.WriteLine("User Log: ");

            foreach (var i in Data.UserCollection)
            {
                string pas = new string('*', i.Password.Length);
                Console.WriteLine($"User:\n {i.Username}, Password: {pas}\n");
            }
            UI.PrintMessage("Press Any Key to Return to Menu...");
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
                Console.Write("Unlock: ");
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
                    UI.ErrorMessage("Can't Contain Numbers. ");
                }
            }
            if (!Data.Currency.ContainsKey(currency) && currency.Length == 3 && !isNumber)
            {

                Console.WriteLine("What's the Current Exchangerate to SEK From This Currency? ");
                if (decimal.TryParse(Console.ReadLine(), out decimal exchange) && exchange > 0)
                {
                    Data.Currency.Add(currency, exchange);
                    Console.WriteLine($"{currency} Was Added");
                    Thread.Sleep(600);
                }
                else
                {
                    UI.ErrorMessage("Wrong Input.");
                    Thread.Sleep(1200);
                }

            }
            else if (Data.Currency.ContainsKey(currency))
            {
                UI.ErrorMessage("This Currency Already Exists. ");
                Thread.Sleep(1200);
            }
            else
            {
                UI.ErrorMessage("Wrong Input, Must Be a Valid ISO Currency-Code. ");
                Thread.Sleep(1200);
            }
        }
        internal void CurrencyUpdate()
        {
            Console.WriteLine("Which Currency Do you Want to Edit? ");
            foreach (var i in Data.Currency)
            {
                Console.WriteLine($"{i.Key} | {i.Value}");
            }



            string choice = Console.ReadLine().ToUpper();
            if (Data.Currency.ContainsKey(choice) && choice != "SEK")
            {
                Console.WriteLine("What Is The New Exchangerate From SEK? ");
                decimal.TryParse(Console.ReadLine(), out decimal value);
                Data.Currency[choice] = value;
                Console.WriteLine($"New Value {choice} - {value}");
                Thread.Sleep(1200);
            }
            else if (choice == "SEK")
            {
                UI.ErrorMessage("You Can't Change This Currency. ");
            }
            else if (!Data.Currency.ContainsKey(choice))
            {
                UI.ErrorMessage("Wrong Input, Must Be a Valid ISO Currency Code. ");
                Thread.Sleep(1200);
            }

        }
        internal void CurrencyRemove()
        {
            Console.WriteLine("Choose the Currency You Want to Remove: ");
            foreach (var i in Data.Currency)
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
                    Console.WriteLine($"{choice} Was Successfully Removed From Our Supported Currencies. ");
                    Thread.Sleep(600);
                }
                else
                {
                    Console.WriteLine("Cancelling, Returning To Menu...");
                    Thread.Sleep(600);
                }
            }
            else
            {
                UI.ErrorMessage($"{choice} Does Not Match Any Currency Supported By TDD. ");
                Thread.Sleep(600);
            }
        }
    }
}