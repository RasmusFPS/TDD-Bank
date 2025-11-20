namespace TDD_Bank
{
    internal class BankTransfer
    {
        internal decimal Amount { get; set; }

        internal static bool TransferToMe(Client client)
        {
            if (client.Accounts.Count < 2)
            {
                Console.WriteLine("inte");
                return false;
            }
            Console.WriteLine("Your accounts:\n");
            foreach (var acc in client.Accounts)
            {
                Console.WriteLine($"Account {acc.AccountNumber}: {acc.Balance} kr");
            }
            return false;
        }

        internal void TransferToOthers(Account)
        {
            Console.WriteLine("Wich account do you want to transfer from?");
            myAccounts = Account.ShowAccounts();
        }

        internal void TransferLog()
        {

        }
    }
}
