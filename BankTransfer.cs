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
