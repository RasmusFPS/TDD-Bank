namespace TDD_Bank
{
    internal class BankTransfer
    {
        internal static bool TransferToMe(Client client)
        {
            bool keepTrying = true;

            while (keepTrying)
            {
                bool success = true;
                UI.ShowAccounts(client);

                Account fromAccount = GetFromAccount(client);
                if (fromAccount == null)
                {
                    success = false;
                }

                Account toAccount = null;
                if (success)
                {
                    toAccount = GetToAccount(client);
                    if (toAccount == null)
                    {
                        success = false;
                    }
                }

                if (success && !ValidateAccounts(fromAccount, toAccount))
                {
                    success = false;
                }

                decimal amount = 0;
                if (success)
                {
                    amount = GetAmount(fromAccount);
                    if(amount == -1)
                    {
                        success = false;
                    }
                }

                if(success && !ValidateBalance (fromAccount, amount))
                {
                    success = false;
                }

                if (success)
                {
                    //Här ska överföringen genomföras.
                    ExecuteTransfer(fromAccount, toAccount, amount);
                    AddTransferLog(fromAccount, toAccount, amount, client, client);
                    Console.WriteLine($"Transfer succeeded. {amount} {fromAccount.Currency} was transferred to accountnumber {toAccount.AccountNumber}.");
                    return true;
                }

                keepTrying = TryAgain();               

                
            }
            return false;
        }

        private static Account GetFromAccount(Client client)
        {
            UI.PrintMessage("Enter wich account you want to transfer from:");
            if (!int.TryParse(Console.ReadLine(), out int fromAccountNumber))
            {
                UI.PrintMessage("Invalid accountnumber.");
                return null;
            }

            Account fromAccount = client.Accounts.Find(firstAccount => firstAccount.AccountNumber == fromAccountNumber);

            if (fromAccount == null)
            {
                UI.PrintMessage("Can't fint the account.");
                return null;
            }

            return fromAccount;
        }

        private static Account GetToAccount(Client client)
        {
            UI.PrintMessage("Enter wich account you want to transfer from:");
            if (!int.TryParse(Console.ReadLine(), out int toAccountNumber))
            {
                UI.PrintMessage("Invalid accountnumber.");
                return null;
            }

            Account toAccount = client.Accounts.Find(firstAccount => firstAccount.AccountNumber == toAccountNumber);

            if (toAccount == null)
            {
                UI.PrintMessage("Can't fint the account.");
                return null;
            }

            return toAccount;

        }

        private static bool ValidateAccounts(Account fromAccount, Account toAccount)
        {
            if (fromAccount.AccountNumber == toAccount.AccountNumber)

            {
                UI.PrintMessage("You can't transfer to the same account.");
                return false;
            }

            return true;
        }

        private static decimal GetAmount(Account fromAccount)
        {
            Console.WriteLine($"How much do you want to transfer? Balance: {fromAccount.Balance} {fromAccount.Currency}");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                UI.PrintMessage("Invalid amount.");//Lägg till fel meddelande bokstäver skrivs in.
                return -1;
            }
            return amount;
        }

        private static bool ValidateBalance(Account fromAccount, decimal amount)
        {
            if (fromAccount.Balance < amount)
            {
                UI.PrintMessage("Insufficient balance.");
                return false;
            }
            return true;
        }
        private static bool TryAgain()
        {
            Console.Write("Do you want to try again? (j/n)");
            return Console.ReadLine().ToLower() == "j";
        }

        //GENOMFÖRA ÖVERFÖRING METOD BEHÖVS NOG HÄR.
        private static void ExecuteTransfer(Account fromAccount, Account toAccount, decimal amount)
        {
            fromAccount.Withdraw(amount);
            if (fromAccount.Currency != toAccount.Currency)
            {
                amount /= Data.Currency[fromAccount.Currency];
                amount *= Data.Currency[toAccount.Currency];
            }
            toAccount.Deposit(amount);
        }

        //returnerar bool för att se om transaktionen lyckades
        internal static bool TransferToOthers(Client sender)
        {
            Console.WriteLine("Your accounts:");
            //Loopar igenom och visar konton
            foreach (var acc in sender.Accounts)
            {
                Console.WriteLine($"Account number: {acc.AccountNumber}");
                Console.WriteLine($"Balance: {acc.Balance}  {acc.Currency}\n");
            }
            Console.WriteLine("Enter the account number of the account you want to transfer from");
            string fromInput = Console.ReadLine();

            Account fromAccount = null;

            //Loopar igenom för att hitta matchande konton
            foreach (var acc in sender.Accounts)
            {
                if (acc.AccountNumber.ToString() == fromInput)
                {
                    //när match hittas sparas det i denna variabeln
                    fromAccount = acc;
                    break;
                }
            }

            //Kontrollerar att konto hittats
            if (fromAccount == null)
            {
                Console.WriteLine("Invalid account");
                return false;
            }

            Console.WriteLine("Enter the account you want to transfer to:");
            string toInput = Console.ReadLine();

            Account toAccount = null;

            //yttre loop kollar alla användare i systemet, inre kollar individuella konton för klienter
            foreach (var user in Data.UserCollection)
            {
                if (user is Client client)
                {
                    foreach (var acc in client.Accounts)
                    {
                        if (acc.AccountNumber.ToString() == toInput)
                        {
                            toAccount = acc;
                            break;
                        }
                    }
                }
                if (toAccount != null)
                    break;
            }

            if (toAccount == null)
            {
                Console.WriteLine("Account not found.");
                return false;
            }

            Console.WriteLine("Enter the amount you want to transfer:");
            string amountInput = Console.ReadLine();

            //om det inte går att konvertera input till decimal -> felmeddelande
            if (!decimal.TryParse(amountInput, out decimal amount))
            {
                Console.WriteLine("Invalid amount.");
                return false;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Amount must be grater than zero.");
                return false;
            }

            if (fromAccount.Balance < amount)
            {
                Console.WriteLine("Insufficient balance.");
                return false;
            }

            fromAccount.Withdraw(amount);

            toAccount.Deposit(amount);

            AddTransferLog(fromAccount, toAccount, amount, sender, sender);

            Console.WriteLine($"Transfer successful! {amount} {fromAccount.Currency} was sent from {fromAccount.AccountNumber} to {toAccount.AccountNumber}.");
            return true;
        }

        internal static void AddTransferLog(Account fromAccount, Account toAccount, decimal amount, User fromUser, User toUser)
        {
            TransferLog log = new TransferLog
            {
                FromAccount = fromAccount.AccountNumber,
                ToAccount = toAccount.AccountNumber,
                Amount = amount,
                Currency = fromAccount.Currency,
                FromUser = fromUser.Username,
                ToUser = toUser.Username,
                LogTime = DateTime.Now

            };

            Data.TransferHistory.Add(log);


        }
    }
}
