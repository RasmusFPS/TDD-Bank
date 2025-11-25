namespace TDD_Bank
{
    internal class BankTransfer
    {
        internal static bool TransferToMe(Client client)
        {
            //if (client.Accounts.Count < 2)
            //{
            //    Console.WriteLine("inte");
            //    return false;
            //}
            Console.WriteLine("Your accounts:\n");
            foreach (var acc in client.Accounts)
            {
                Console.WriteLine($"Account {acc.AccountNumber}: {acc.Balance} {acc.Currency}");
            }
            ////return false;//Ev ta bort?

            Console.WriteLine("Enter wich account you want to transfer from:");
            if (!int.TryParse(Console.ReadLine(), out int fromAccountNumber))
            {
                Console.WriteLine("Invalid accountnumber.");
                return false;
            }

            Account fromAccount = client.Accounts.Find(firstAccount => firstAccount.AccountNumber == fromAccountNumber);
            if (fromAccount == null)
            {
                Console.WriteLine("Can't fint the account.");
                return false;
            }

            Console.WriteLine("Enter wich account you want to transfer to:");
            if (!int.TryParse(Console.ReadLine(), out int toAccountNumber))
            {
                Console.WriteLine("Invalid accountnumber.");
                return false;
            }

            Account toAccount = client.Accounts.Find(secondAccount => secondAccount.AccountNumber == toAccountNumber);
            if (toAccount == null)
            {
                Console.WriteLine("Can't find the account.");
                return false;
            }

            if (fromAccountNumber == toAccountNumber)
            {
                Console.WriteLine("You can't transfer to the same account.");
                return false;
            }

            //if (fromAccount.Currency != toAccount.Currency)
            //{
            //    Console.WriteLine("The accounts have different currencies, transfer not possible.");
            //    return false;
            //}

            if (fromAccount.Currency != toAccount.Currency)
            {
                Exchange exchange = new Exchange();
                exchange.ExchangeStart(toAccount);
            }

                Console.WriteLine($"How much do you want to transfer? Saldo: {fromAccount.Balance} {fromAccount.Currency}");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount.");
                return false;
            }

            if (fromAccount.Balance < amount)
            {
                Console.WriteLine("Insufficient balance.");
                return false;
            }

            //Här ska överföringen genomföras.
            ExecuteTransfer(fromAccount, toAccount, amount, client.Username, client.Username);
            Console.WriteLine($"Transfer succeeded. {amount} {fromAccount.Currency} was transferred.");
            return true;
        }

        //GENOMFÖRA ÖVERFÖRING METOD BEHÖVS NOG HÄR.
        private static void ExecuteTransfer(Account fromAccount, Account toAccount, decimal amount, string fromUser, string toUser)
        {
            fromAccount.Withdraw(amount);
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

            Console.WriteLine($"Transfer successful! {amount} {fromAccount.Currency} was sent from {fromAccount.AccountNumber} to {toAccount.AccountNumber}.");
            return true;
        }

        internal void TransferLog()
        {

        }
    }
}
