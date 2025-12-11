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
            bool create = true;
            while (create)
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
                    if (password.Length >= 3)
                    {

                        bool isAdmin = false;
                        int tries = 3;
                        bool isLocked = false;

                        Data.UserCollection.Add(new Client(name, password, isAdmin, tries, false));

                        foreach (var i in Data.UserCollection)
                        {
                            string pas = new string('*', i.Password.Length);

                            UI.PrintMessage($"User:\n {i.Username}, Password: {pas}\n");
                        }
                        create = false;
                        UI.PrintMessage("Press Enter to Return to Menu");
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        UI.ErrorMessage("Password Must Be Atleast 3 Characters");
                        Thread.Sleep(1200);
                    }
                }
                else
                {
                    UI.ErrorMessage("Name Already Taken.");
                    Thread.Sleep(1200);
                    if (!UI.AskTryagain())
                    {
                        create = false;
                    }
                }

                UI.PrintMessage("Press Any Key to Return to Menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }


        internal void UserLog()
        {
            UI.PrintMessage("User Log: ");

            foreach (var i in Data.UserCollection)
            {
                string pas = new string('*', i.Password.Length);
                UI.PrintMessage($"User:\n {i.Username}, Password: {pas}\n");
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
                    UI.PrintMessage($" {nr}. {i}");
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
                UI.PrintMessage("No Locked Users.");
                Console.ReadKey();
            }
        }
        internal void AddCurrency()
        {
            bool isNumber = false;
            UI.PrintMessage("What Currency Would You Like to Add? ");
            string currency = Console.ReadLine().ToUpper();
            foreach (char i in currency)
            {
                if (char.IsNumber(i))
                {
                    isNumber = true;
                    UI.ErrorMessage("Can't Contain Numbers. ");
                    Thread.Sleep(1200);
                }
            }
            if (!Data.Currency.ContainsKey(currency) && currency.Length == 3 && !isNumber)
            {

                UI.PrintMessage("What's the Current Exchangerate to SEK From This Currency? ");
                if (decimal.TryParse(Console.ReadLine(), out decimal exchange) && exchange > 0)
                {
                    Data.Currency.Add(currency, exchange);
                    UI.SuccessMessage($"{currency} Was Added");
                    Thread.Sleep(1200);
                }
                else if (exchange == 0)
                {
                    UI.ErrorMessage("Can't Have 0 As a Exchangerate");
                    Thread.Sleep(1200);

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
            UI.PrintMessage("Which Currency Do you Want to Edit?");
            foreach (var i in Data.Currency)
            {
                UI.PrintMessage($"{i.Key} | {i.Value}");
            }



            string choice = Console.ReadLine().ToUpper();
            if (Data.Currency.ContainsKey(choice) && choice != "SEK")
            {
                UI.PrintMessage("What Is The New Exchangerate From SEK?");
                decimal.TryParse(Console.ReadLine(), out decimal value);
                Data.Currency[choice] = value;
                UI.PrintMessage($"New Value {choice} - {value}");
                Thread.Sleep(1200);
            }
            else if (choice == "SEK")
            {
                UI.ErrorMessage("You Cannot Change This Currency");
                Thread.Sleep(1200);
            }
            else if (!Data.Currency.ContainsKey(choice))
            {
                UI.ErrorMessage("Wrong Input, Must Be a Valid ISO Currency Code. ");
                Thread.Sleep(1200);
            }

        }
        internal void CurrencyRemove()
        {
            UI.PrintMessage("Choose the Currency You Want to Remove: ");
            foreach (var i in Data.Currency)
            {
                UI.PrintMessage(i.Key);
            }
            string choice = Console.ReadLine().ToUpper();
            if (Data.Currency.ContainsKey(choice))
            {
                UI.PrintMessage($"Are You Sure You Want To Remove {choice}? y/n ");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    Data.Currency.Remove(choice);
                    UI.SuccessMessage($"{choice} Was Successfully Removed From Our Supported Currencies.");
                    Thread.Sleep(1200);
                }
                else
                {
                    UI.PrintMessage("Cancelling, Returning To Menu...");
                    Thread.Sleep(1200);
                }
            }
            else
            {
                UI.ErrorMessage($"{choice} Does Not Match Any Currency Supported By TDD. ");
                Thread.Sleep(1200);
            }
        }
    }
}