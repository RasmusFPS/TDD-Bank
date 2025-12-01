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

        /*
        internal bool ApplyForLoan(Client client)
        {
            decimal total = 0;
            bool validAmount = false;

            while (!validAmount)
            {

                if (client.Accounts.Count < 1)
                {
                    UI.PrintMessage("Loan cannot be processed.");
                    return false;
                }

                foreach (Account account in client.Accounts)
                {
                    total = +account.Balance;
                }

                decimal maxLoan = total * 5;
                UI.PrintMessage($"Your total balance is {total} kr");//HUR KOMMER JAG ÅT CURRENCY?
                UI.PrintMessage($"You can borrow a maximum of: {maxLoan} kr");

                UI.AskQuestion("How much do you want to borrow?");
                if (!int.TryParse(Console.ReadLine(), out int loanAmount))
                {
                    UI.PrintMessage("Invalid amount. Try again.");
                    continue;
                }

                if (loanAmount <= 0)
                {
                    UI.PrintMessage("The amount must be greater than 0. Try again");
                    continue;
                }

                if (loanAmount > maxLoan)
                {
                    UI.PrintMessage($"Your loan limit is {maxLoan} kr.");//VILL KOMMA ÅT CURRENCY
                    continue;
                }

                validAmount = true;

                decimal interest = loanAmount * Data._loanInterest;
                decimal totalToPay = loanAmount + interest;

                UI.PrintMessage($"Loan amount: {loanAmount} kr" +
                    $"Interest rate: {interest} kr" +
                    $"Total to pay: {totalToPay} kr" +
                    $"Do you want to take the loan? Enter yes och no.");
                string answer = Console.ReadLine().ToLower();

                if (answer == "yes")
                {
                    UI.ShowAccounts(client);
                    //Ta lånet
                    return true;
                }
                else
                {
                    return false;
                }

            }
        } */
    }
}
