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
                int fromAccountNumber;
                while (!int.TryParse(Console.ReadLine(), out fromAccountNumber))
                {
                    UI.PrintMessage("Invalid accountnumber.");
                }
                //en metod som söker igenom listan och 
                //returnerar det första elementet som matchar ett villkor, typ ihoptryckt foreachloop?
                Account fromAccount = client.Accounts.Find(firstAccount => firstAccount.AccountNumber == fromAccountNumber);
                if (fromAccount == null)
                {
                    UI.PrintMessage("Can't fint the account.");
                    TryAgain();
                }

                Console.WriteLine("Enter which account you want to transfer to:");
                int toAccountNumber;
                while (!int.TryParse(Console.ReadLine(), out toAccountNumber))
                {
                    UI.PrintMessage("Invalid accountnumber.");
                }

                Account toAccount = client.Accounts.Find(secondAccount => secondAccount.AccountNumber == toAccountNumber);
                if (toAccount == null)
                {
                    UI.PrintMessage("Can't find the account.");
                    TryAgain();
                }

                if (fromAccountNumber == toAccountNumber)
                {
                    UI.PrintMessage("You can't transfer to the same account.");
                }

                Console.WriteLine($"How much do you want to transfer? Balance: {fromAccount.Balance} {fromAccount.Currency}");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
                {
                    UI.PrintMessage("Invalid amount.");
                }

                if (fromAccount.Balance < amount)
                {
                    UI.PrintMessage("Insufficient balance.");
                    TryAgain();
                }

                //Här ska överföringen genomföras.
                ExecuteTransfer(fromAccount, toAccount, amount);
                Console.WriteLine($"Transfer succeeded. {amount} {fromAccount.Currency} was transferred to accountnumber {toAccount.AccountNumber}.");
                return true;
            }
            return false;
        }

        private static void ExecuteTransfer(Account fromAccount, Account toAccount, decimal amount)
        {
            //GÖR EXCHANGE KONTROLLEN HÄR konverteringen sker här
            fromAccount.Withdraw(amount, fromAccount);
            toAccount.Deposit(amount, toAccount);
        }

        private static bool TryAgain()
        {
            Console.Write("Do you want to try again? (j/n) ");
            return Console.ReadLine().Trim().ToLower() == "j";
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

            Console.WriteLine($"Transfer successful! {amount} {fromAccount.Currency} was sent from {fromAccount.AccountNumber} to {toAccount.AccountNumber}.");
            return true;
        }

        internal void TransferLog()
        {

        }
    }
}
