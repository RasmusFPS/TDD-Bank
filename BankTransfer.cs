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

                Account? fromAccount = GetFromAccount(client);
                if (fromAccount == null)
                {
                    success = false;
                }

                Account? toAccount = null;
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
                    UI.PrintMessage($"Transfer Successful! {amount} {fromAccount.Currency} Was Transferred From Account {fromAccount.AccountNumber} to Account {toAccount.AccountNumber}.");
                    UI.PrintMessage("Press Enter to Return to Menu...");
                    Console.ReadKey();
                    return true;
                }

                keepTrying = UI.AskTryagain();               
                                
            }
            return false;
        }

        private static Account? GetFromAccount(Client client)
        {
            UI.PrintMessage("Enter Wich Account You Want to Transfer From: ");
            if (!int.TryParse(Console.ReadLine(), out int fromAccountNumber))
            {
                UI.ErrorMessage("Invalid Accountnumber.");
                return null;
            }

            Account? fromAccount = client.Accounts.Find(firstAccount => firstAccount.AccountNumber == fromAccountNumber);

            if (fromAccount == null)
            {
                UI.ErrorMessage("Can't Find the Account.");
                return null;
            }

            return fromAccount;
        }

        private static Account GetToAccount(Client client)
        {
            UI.PrintMessage("Enter Which Account You Want to Transfer too: ");
            if (!int.TryParse(Console.ReadLine(), out int toAccountNumber))
            {
                UI.ErrorMessage("Invalid Accountnumber.");
                return null;
            }

            Account? toAccount = client.Accounts.Find(firstAccount => firstAccount.AccountNumber == toAccountNumber);

            if (toAccount == null)
            {
                UI.ErrorMessage("Can't Find the Account.");
                return null;
            }

            return toAccount;
        }

        private static bool ValidateAccounts(Account fromAccount, Account toAccount)
        {
            if (fromAccount.AccountNumber == toAccount.AccountNumber)
            {
                UI.ErrorMessage("You Can't Transfer to The Same Account.");
                return false;
            }

            return true;
        }

        private static decimal GetAmount(Account fromAccount)
        {
            UI.PrintMessage($"How Much do You Want to Transfer? Balance: {fromAccount.Balance} {fromAccount.Currency}");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                UI.ErrorMessage("Invalid Input - Please Enter a Number.");
                return -1;
            }
            else if(amount <= 0)
            {
                UI.ErrorMessage("Amount Must be Greater Than 0.");
                return -1;
            }
                return amount;
        }

        private static bool ValidateBalance(Account fromAccount, decimal amount)
        {
            if (fromAccount.Balance < amount)
            {
                UI.ErrorMessage("Insufficient Balance.");
                return false;
            }
            return true;
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

                //choose account to send from
                Account? fromAccount = GetFromAccount(sender);
                if (fromAccount == null)
                {
                    keepTrying = true;
                    continue;
                }

                //choose account to send to
                Client reciver;
                Account toAccount = GetToAccontOtherClient(out reciver);
                if (toAccount == null)
                {
                    keepTrying = UI.AskTryagain();
                    continue;
                }

                //calling method to Validates accounts
                if (!ValidateTransfer(sender, fromAccount, toAccount, reciver))
                {
                    keepTrying = UI.AskTryagain();
                    continue;
                }

                //amount input
                decimal amount = GetAmount(fromAccount);
                if (amount <= 0)
                {
                    keepTrying = UI.AskTryagain();
                    continue;
                }

                //checking balance
                if (!ValidateBalance(fromAccount, amount))
                {
                    keepTrying = UI.AskTryagain();
                    continue;
                }

                //Go thru with transfer
                ExecuteTransfer(fromAccount, toAccount, amount);
                AddTransferLog(fromAccount, toAccount, amount, sender, reciver);
                
                UI.PrintMessage($"Transfer Successful! {amount} {fromAccount.Currency} Was Sent From Account {fromAccount.AccountNumber} to Account {toAccount.AccountNumber}.");
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

            //check to see if reciver account is same as sender acocunt
            if (fromAccount.AccountNumber == toAccount.AccountNumber)
            {
                UI.ErrorMessage("Can't Transfer to The Same Account.");
                return false;
            }

            //check if sender and reciver accounts belongs to same client
            if (sender == reciver)
            {
                UI.ErrorMessage("Can't Transfer to Your Own Account. Go to Transfer to Me.");
                return false;
            }
            return true;
        }

        private static Account GetToAccontOtherClient(out Client reciver)
        {
            reciver = null;

            UI.PrintMessage("Enter Which Account You Want to Transfer too: ");
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
            UI.ErrorMessage("Account Not Found.");
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
