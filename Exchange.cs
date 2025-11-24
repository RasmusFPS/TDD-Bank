using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Exchange
    {
        public decimal sek;
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
        /*internal void ValueExchangeSek()
        {
            if (Account.Currency == USD)
            {
                sek = Account.Balance / 0.105m;
            }
            else if (Account.Currency == EUR)
            {
                sek = Account.Balance / 0.091m;
            }
            else if (Account.Currency == DKK)
            {
                sek = Account.Balance / 0.68m;
            }
        }*/

    }
}
