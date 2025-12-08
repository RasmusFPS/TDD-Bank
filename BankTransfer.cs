using System.Net.Http.Headers;

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
                    if (amount == -1)
                    {
                        success = false;
                    }
                }

                if (success && !ValidateBalance(fromAccount, amount))
                {
                    success = false;
                }

                if (success)
                {
                    ExecuteTransfer(fromAccount, toAccount, amount);
                    AddTransferLog(fromAccount, toAccount, amount, client, client);
                    UI.PrintMessage($"Transfer successful! {amount} {fromAccount.Currency} was transferred from account {fromAccount.AccountNumber} to account {toAccount.AccountNumber}.");
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
                UI.ErrorMessage("Invalid accountnumber.");
                return null;
            }

            Account fromAccount = client.Accounts.Find(firstAccount => firstAccount.AccountNumber == fromAccountNumber);

            if (fromAccount == null)
            {
                UI.ErrorMessage("Can't find the account.");
                return null;
            }

            return fromAccount;
        }

        private static Account GetToAccount(Client client)
        {
            UI.PrintMessage("Enter wich account you want to transfer to:");
            if (!int.TryParse(Console.ReadLine(), out int toAccountNumber))
            {
                UI.ErrorMessage("Invalid accountnumber.");
                return null;
            }

            Account toAccount = client.Accounts.Find(firstAccount => firstAccount.AccountNumber == toAccountNumber);

            if (toAccount == null)
            {
                UI.ErrorMessage("Can't find the account.");
                return null;
            }

            return toAccount;
        }

        private static bool ValidateAccounts(Account fromAccount, Account toAccount)
        {
            if (fromAccount.AccountNumber == toAccount.AccountNumber)
            {
                UI.ErrorMessage("You can't transfer to the same account.");
                return false;
            }

            return true;
        }

        private static decimal GetAmount(Account fromAccount)
        {
            UI.PrintMessage($"How much do you want to transfer? Balance: {fromAccount.Balance} {fromAccount.Currency}");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                UI.ErrorMessage("Invalid input - please enter a number.");
                return -1;
            }
            else if(amount <= 0)
            {
                UI.ErrorMessage("Amount must be greater than 0.");
                return -1;
            }
                return amount;
        }

        private static bool ValidateBalance(Account fromAccount, decimal amount)
        {
            if (fromAccount.Balance < amount)
            {
                UI.ErrorMessage("Insufficient balance.");
                return false;
            }
            return true;
        }
        private static bool TryAgain()
        {
            UI.PrintMessage("Do you want to try again? (j/n)");
            return Console.ReadLine().ToLower() == "j";
        }

        private static void ExecuteTransfer(Account fromAccount, Account toAccount, decimal amount)
        {
            fromAccount.Withdraw(amount);
            if (fromAccount.Currency != toAccount.Currency)
            {
                amount /= Data.Currency[fromAccount.Currency];
                amount *= Data.Currency[toAccount.Currency];
            }

            PendingTrans newTransfer = new PendingTrans
            {
                ToAccount = toAccount,
                Amount = amount
            };

            Data.TransferQueue.Add(newTransfer);
        }

        internal static void CheckQueue()
        {
            if(DateTime.Now >= Data.Runtime)
            {
                foreach(var transfer in Data.TransferQueue)
                {
                    transfer.ToAccount.Deposit(transfer.Amount);
                }

                Data.TransferQueue.Clear();

                Data.Runtime = DateTime.Now.AddMinutes(15);
            }
        }

        //returnerar bool för att se om transaktionen lyckades 
        internal static bool TransferToOthers(Client sender)
        {
            bool keepTrying = true;

            while (keepTrying)
            {
                UI.ShowAccounts(sender);

                //välj konto att skicka från
                Account fromAccount = GetFromAccount(sender);
                if (fromAccount == null)
                {
                    keepTrying = true;
                    continue;
                }

                //välj konto att skicka till
                Client reciver;
                Account toAccount = GetToAccontOtherClient(out reciver);
                if (toAccount == null)
                {
                    keepTrying = TryAgain();
                    continue;
                }

                //validera konton
                if (!ValidateTransfer(sender, fromAccount, toAccount, reciver))
                {
                    keepTrying = TryAgain();
                    continue;
                }

                //Ange belopp att överföra
                decimal amount = GetAmount(fromAccount);
                if (amount <= 0)
                {
                    keepTrying = TryAgain();
                    continue;
                }

                //checking balance
                if (!ValidateBalance(fromAccount, amount))
                {
                    keepTrying = TryAgain();
                    continue;
                }

                //Go thru with transfer
                ExecuteTransfer(fromAccount, toAccount, amount);
                AddTransferLog(fromAccount, toAccount, amount, sender, reciver);
                
                UI.PrintMessage($"Transfer successful! {amount} {fromAccount.Currency} was sent from account {fromAccount.AccountNumber} to account {toAccount.AccountNumber}.");
                return true;
            }

            return false;
        }

        private static bool ValidateTransfer(Client sender, Account fromAccount, Account toAccount, Client reciver)
        {
            if (toAccount == null || reciver == null)
            {
                return false;
            }

            //kollar så det inte är samma konto
            if (fromAccount.AccountNumber == toAccount.AccountNumber)
            {
                UI.ErrorMessage("Can't transfer to the same account.");
                return false;
            }

            //kollar att det inte är samma kund
            if (sender == reciver)
            {
                UI.ErrorMessage("Can't transfer to your own account. Go to Transfer to me.");
                return false;
            }
            return true;
        }

        private static Account GetToAccontOtherClient(out Client reciver)
        {
            reciver = null;

            UI.PrintMessage("Enter the accountnumber of the acoount you want to transfer to:");
            string toInput = Console.ReadLine();

            foreach (var user in Data.UserCollection)
            {
                if (user is Client client)
                {
                    var toAccount = client.Accounts.FirstOrDefault(acc => acc.AccountNumber.ToString() == toInput);
                    if (toAccount !=null)
                    {
                        reciver = client;
                        return toAccount;
                    }
                }
            }
            UI.ErrorMessage("Account not found.");
            return null;
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
