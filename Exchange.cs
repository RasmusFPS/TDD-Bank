using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Exchange
    {
        public decimal startValue;
        public decimal sek;
        internal void ExchangeStart(Account account)
        {
            if (account.Currency != "SEK")
            {
                startValue = account.Balance;
                Exchange.ValueExchangeSek(account, sek, startValue);
            }
            else if (account.Currency == "SEK")
            {

            }
        }
        internal void ValueExchangeDollar()
        {
            decimal dollar;
            dollar = sek * 0.105m;
        }
        internal void ValueExchangeEuro()
        {
            decimal dollar;
            dollar = sek * 0.091m;
        }
        internal void ValueExchangeDkk()
        {
            decimal dollar;
            dollar = sek * 0.68m;
        }
        internal static void ValueExchangeSek(Account account, decimal sek, decimal startValue)
        {
            if (account.Currency == "USD")
            {
                startValue = startValue / 0.105m;

                sek = account.Balance / 0.105m;
            }
            else if (account.Currency == "EUR")
            {
                sek = account.Balance / 0.091m;
            }
            else if (account.Currency == "DKK")
            {
                sek = account.Balance / 0.68m;
            }
        }

    }
}
