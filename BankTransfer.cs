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
                Console.WriteLine($"Account {acc.AccountNumber}: {acc.Balance} kr {acc.Currency}");
            }
            ////return false;//Ev ta bort?

            Console.WriteLine("Enter wich account you want to transfer from:");
            if (!int.TryParse(Console.ReadLine(), out int fromIndex) || fromIndex < 1 || fromIndex > client.Accounts.Count)
            {
                Console.WriteLine("Invalid choice.");
                return false;
            }

            Console.WriteLine("Enter wich account you want to transfer to:");
            if (!int.TryParse(Console.ReadLine(), out int toIndex) || toIndex < 1 || toIndex > client.Accounts.Count)
            {
                Console.WriteLine("Invalid choice.");
                return false;
            }

            if (fromIndex == toIndex)
            {
                Console.WriteLine("You can't transfer to the same account.");
                return false;
            }

            Account fromAccount = client.Accounts[fromIndex - 1];
            Account toAccount = client.Accounts[toIndex - 1];

            if(fromAccount.Currency != toAccount.Currency)
            {
                Console.WriteLine("The accounts have different currencies, transfer not possible.");
                return false;
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

        internal void TransferToOthers(Account account)
        {
            Console.WriteLine("Wich account do you want to transfer from?");
            //myAccounts = Account.ShowAccounts();
        }

        internal void TransferLog()
        {

        }
    }
}
