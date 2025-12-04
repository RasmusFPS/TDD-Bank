namespace TDD_Bank
{
    internal class Loan
    {
        internal int LoanId { get; set; }
        public string ClientUsername { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal TotalToPay { get; set; }
        //Datetime här?
        public bool IsPaidOff { get; set; }

        private static int nextLoanId = 1;


        internal void ApplyForLoan(Client client)
        {
            Account chosenAccount = SelectAccount(client);
            if (chosenAccount == null)
            {
                return;
            }

            decimal maxLoan = CalculateMaxLoan(chosenAccount);
            decimal? requestedAmount = AskLoanAmount(maxLoan);

            if (requestedAmount == null)
            {
                return;
            }

            decimal interest = CalculateInterest(requestedAmount.Value);
            decimal totalPay = CalculateTotalPay(requestedAmount.Value);

            UI.PrintMessage($"Loan amount: {requestedAmount.Value} {chosenAccount.Currency}");
            UI.PrintMessage($"interest: {interest} {chosenAccount.Currency}");
            UI.PrintMessage($"Total to pay: {totalPay} {chosenAccount.Currency}");

            DepositLoan(chosenAccount, requestedAmount.Value);
            Loan newLoan = CreateLoan(client, requestedAmount.Value, totalPay);

            UI.PrintMessage($"loan deposited to account {chosenAccount.AccountNumber}.");
        }
        private Account SelectAccount(Client client)
        {
            UI.PrintMessage("Choose an account to deposit the loan to (cannot be a savings account)");
            UI.ShowAccounts(client);

            int accountNumber = UI.GetAccountNumber();
            Account chosenAccount = client.GetAccount(accountNumber);

            if (chosenAccount == null)
            {
                UI.PrintMessage("Account not found. Loan declined");
                return null;
            }

            if (chosenAccount is SavingAccount)
            {
                UI.ErrorMesage("Cannot take loan on a savings account");
                return null;
            }

            return chosenAccount;
        }

        private decimal CalculateMaxLoan(Account account)
        {
            //konverterar först till SEK
            decimal balanceInSEK = account.Balance / Data.Currency[account.Currency];
            decimal maxLoanInSEK = balanceInSEK * 5;

            //konverterar tillbaka till kontots valuta och returnerar det värdet
            return maxLoanInSEK * Data.Currency[account.Currency];
        }

        private decimal? AskLoanAmount(decimal maxLoan)
        {
            UI.PrintMessage($"Enter loan amount (max: {maxLoan}):");
            string? Input = Console.ReadLine();

            if (decimal.TryParse(Input, out decimal requestedLoan))
            {
                if (requestedLoan > 0 && requestedLoan <= maxLoan)
                {
                    return requestedLoan;
                }
            }
            UI.ErrorMesage("Invalid amount. Loan declined");
            return null;
        }

        private decimal CalculateInterest(decimal amount)
        {
            return amount * Data._loanInterest;
        }

        private decimal CalculateTotalPay(decimal amount)
        {
            return amount + CalculateInterest(amount);
        }

        private void DepositLoan(Account account, decimal amount)
        {
            account.Deposit(amount);
        }

        private Loan CreateLoan(Client client, decimal amount, decimal totalToPay)
        {
            Loan newLoan = new Loan
            {
                LoanId = nextLoanId++,
                ClientUsername = client.Username,
                Amount = amount,
                InterestRate = Data._loanInterest,
                TotalToPay = totalToPay,
                IsPaidOff = false
            };
            client.Loans.Add(newLoan);
            Data.ActiveLoans.Add(newLoan);

            return newLoan;
        }
    }
}
