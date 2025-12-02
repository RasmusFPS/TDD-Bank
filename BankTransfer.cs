namespace TDD_Bank
{
    internal class BankTransfer
    {
        internal static bool TransferToMe(Client client)
        {
            bool keepTrying = true;

            while (keepTrying)
            {
                UI.ShowAccounts(client);

                UI.PrintMessage("Enter wich account you want to transfer from:");
                if (!int.TryParse(Console.ReadLine(), out int fromAccountNumber))
                {
                    UI.PrintMessage("Invalid accountnumber.");
                    continue;
                }

                Account fromAccount = client.Accounts.Find(firstAccount => firstAccount.AccountNumber == fromAccountNumber);
                if (fromAccount == null)
                {
                    UI.PrintMessage("Can't fint the account.");
                    Console.Write("Do you want to try again? (j/n)");
                    keepTrying = Console.ReadLine().ToLower() == "j";
                    continue;
                }

                Console.WriteLine("Enter which account you want to transfer to:");
                if (!int.TryParse(Console.ReadLine(), out int toAccountNumber))
                {
                    UI.PrintMessage("Invalid accountnumber.");
                    continue;
                }

                Account toAccount = client.Accounts.Find(secondAccount => secondAccount.AccountNumber == toAccountNumber);
                if (toAccount == null)
                {
                    UI.PrintMessage("Can't find the account.");
                    Console.Write("Do you want to try again? (j/n)");
                    keepTrying = Console.ReadLine().ToLower() == "j";
                    continue;
                }

                if (fromAccountNumber == toAccountNumber)
                {
                    UI.PrintMessage("You can't transfer to the same account.");
                    continue;
                }

                Console.WriteLine($"How much do you want to transfer? Balance: {fromAccount.Balance} {fromAccount.Currency}");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
                {
                    UI.PrintMessage("Invalid amount.");
                    continue;
                }

                if (fromAccount.Balance < amount)
                {
                    UI.PrintMessage("Insufficient balance.");
                    Console.Write("Do you want to try again? (j/n)");
                    keepTrying = Console.ReadLine().ToLower() == "j";
                    continue;
                }

                //Här ska överföringen genomföras.
                ExecuteTransfer(fromAccount, toAccount, amount);
                AddTransferLog(fromAccount, toAccount, amount, client, client);
                Console.WriteLine($"Transfer succeeded. {amount} {fromAccount.Currency} was transferred to accountnumber {toAccount.AccountNumber}.");
                return true;
            }
            return false;
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

            Account fromAccount = sender.Accounts.FirstOrDefault(acc => acc.AccountNumber.ToString() == fromInput);
            if (fromAccount == null)
            {
                Console.WriteLine("Invalid account");
                return false;
            }

            Console.WriteLine("Enter the account you want to transfer to:");
            string toInput = Console.ReadLine();

            Account toAccount = null;
            Client reciver = null;

            foreach (var user in Data.UserCollection)
            {
                if (user is Client client)
                {
                   
                    toAccount = client.Accounts.FirstOrDefault(acc => acc.AccountNumber.ToString() == toInput);
                    if (toAccount != null)
                        {
                        reciver = client;
                            break;
                        }
                    
                }
            }

            if (toAccount == null)
            {
                Console.WriteLine("Account not found.");
                return false;
            }

            if (fromAccount.AccountNumber == toAccount.AccountNumber)
            {
                Console.WriteLine("Cannot transfer to the same account.");
                return false;
            }
           
            Console.WriteLine("Enter the amount you want to transfer:");
            if(!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return false;
            }

            if (fromAccount.Balance < amount)
            {
                Console.WriteLine("Insufficient balance.");
                return false;
            }

            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);

            AddTransferLog(fromAccount, toAccount, amount, sender, reciver);

            Console.WriteLine($"Transfer successful! {amount} {fromAccount.Currency} was sent from account{fromAccount.AccountNumber} to account {toAccount.AccountNumber}.");
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
