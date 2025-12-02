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

        internal bool ApplyForLoan(Client client)
        {
            decimal total = 0;
            bool validAmount = false;

            if (client.Accounts.Count < 1)
            {
                UI.PrintMessage("Loan cannot be processed.");
                return false;
            }

            foreach (Account account in client.Accounts)
            {
                total += account.Balance;
            }

            decimal maxLoan = total * 5;
            UI.PrintMessage($"Your total balance is {total} kr");//HUR KOMMER JAG ÅT CURRENCY?
            UI.PrintMessage($"You can borrow a maximum of: {maxLoan} kr");

            int loanAmount;
            UI.PrintMessage("How much do you want to borrow?");
            while (!int.TryParse(Console.ReadLine(), out loanAmount) || loanAmount <= 0 || loanAmount > maxLoan)//Flytta över till UI???
            {
                
                if (loanAmount <= 0)
                {
                    UI.PrintMessage("The amount must be greater than 0 and in numbers. Try again");
                }

                else if (loanAmount > maxLoan)
                {
                    UI.PrintMessage($"Your loan limit is {maxLoan} kr.");//VILL KOMMA ÅT CURRENCY
                }
                else
                {
                    UI.PrintMessage("Invalid amount. Try again.");
                }
            }

            decimal interest = loanAmount * Data._loanInterest;
            decimal totalToPay = loanAmount + interest;

            UI.PrintMessage($"Loan amount: {loanAmount} kr" +
                $"Interest rate: {interest} kr" +
                $"Total to pay: {totalToPay} kr" +
                $"Do you want to take the loan? Enter yes och no.");
            string answer = Console.ReadLine().ToLower();

            if (answer == "yes")
            {
                //Är jag klar här?
                UI.ShowAccounts(client);
                UI.PrintMessage("Choose accountnumber: ");
                if (!int.TryParse(Console.ReadLine(), out int fromAccountNumber))
                {
                    UI.PrintMessage("Invalid accountnumber.");
                }
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
