using System.ComponentModel;
using System.Security.AccessControl;

namespace TDD_Bank
{
    internal class Loan
    {
        public string ClientUsername { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal TotalToPay { get; set; }
        public DateTime LoanDate { get; set; }
        public string Currency { get; set; }


        public Loan(string clientUsername, decimal amount, decimal interestRate)
        {
            ClientUsername = clientUsername;
            Amount = amount;
            InterestRate = interestRate;
            Currency = "SEK";
            TotalToPay = CalculateTotalToPay();
            LoanDate = DateTime.Now;


        }

        private decimal CalculateTotalToPay()
        {
            //total amount to be repaid
            return Amount + (Amount * InterestRate);
        }

        internal static decimal CalculateBalanceInSek(Client client)
        {
            decimal totalBalance = 0;
            foreach (var account in client.Accounts)
            {
                decimal rate = Data.Currency[account.Currency];
                decimal balanceInSek = account.Balance * rate;
                totalBalance += balanceInSek;
            }

            return totalBalance;
        }

        internal static decimal Borrow(Client client, decimal maxLoan)
        {
            decimal loanRequest = 0;
            decimal totalBalance = CalculateBalanceInSek(client);
            UI.PrintMessage($"Your total balance is {totalBalance} SEK");
            UI.PrintMessage($"You can take a loan of {maxLoan} SEK (five times your balance)");
            //Felmeddelande kring lån som är i fel valuta

            UI.PrintMessage("How much do you want to borrow?");

            while (!decimal.TryParse(Console.ReadLine(), out loanRequest) || loanRequest <= 0 || loanRequest > maxLoan)
            {
                UI.ErrorMesage("Invalid amount.");
                UI.ErrorMesage($"Enter valid numbers and choose a loan under {maxLoan} SEK.");
            }
            return loanRequest;
        }

        internal static void ShowSummary(Loan newLoan)
        {
            decimal interest = newLoan.TotalToPay - newLoan.Amount;
            decimal totalToPay = newLoan.TotalToPay;

            UI.PrintMessage($"Loan amount: {newLoan.Amount} {newLoan.Currency}" +
                        $"\nInterest: {interest} {newLoan.Currency}" +  //RÄNTAN
                        $"\nTotal to pay: {totalToPay} {newLoan.Currency}" +
                        $"\nDo you want to take the loan? Enter yes or no."); //FELHANTERING

        }

        internal static Account FindAccount(Client client)
        {
            UI.ShowAccounts(client);

            Account foundAccount = null;
            while (foundAccount == null)
            {
                UI.PrintMessage("Enter the account number to deposit the loan into: ");
                if (!int.TryParse(Console.ReadLine(), out int accountNumberChoice))
                {
                    UI.ErrorMesage("Invalid account number");
                    continue;
                }

                foreach (var account in client.Accounts)
                {
                    if (account.AccountNumber == accountNumberChoice)
                    {
                        foundAccount = account;
                        break;
                    }
                }

                if (foundAccount == null)
                {
                    UI.ErrorMesage("Try again...");
                    continue;
                }
               
                if (foundAccount is SavingAccount)
                {
                    UI.ErrorMesage("Cannot take loan on a savings account");
                    foundAccount = null;
                }
            }

            return foundAccount;

        }

        internal static bool ApplyForLoan(Client client)
        {
            decimal totalBalance = CalculateBalanceInSek(client);

            if (totalBalance <= 0)
            {
                UI.ErrorMesage("Insufficient balance. Loan declined");
                return false;
            }

            decimal maxLoan = totalBalance * 5;

            decimal loanRequest = Borrow(client, maxLoan);

            var newLoan = new Loan(client.Username, loanRequest, Data._loanInterest);

            ShowSummary(newLoan);

            string loanAnswer = Console.ReadLine().ToLower();

            if (loanAnswer != "yes")
            {
                UI.PrintMessage("Loan cancelled");
                return true;
            }


            Account selectAccount = FindAccount(client);
            while (selectAccount == null)
            {
                UI.ErrorMesage("Account not found.");
                return false;
            }
            selectAccount.Deposit(loanRequest);


            UI.PrintMessage($"The loan ({loanRequest} {newLoan.Currency}) has been deposited {newLoan.LoanDate}");

            Data.ActiveLoans.Add(newLoan);

            return true;
        }

    }
}
