using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class Exchange
    {
        //Exchanges amount  to SEK (inSEK), does not return values
        internal decimal  WithdrawExchange(Account account, decimal amount)
        {
            return amount / Data.Currency[account.Currency];
        }
        //Exchanges inSEK to specified currency, return values
        internal decimal DepositExchange(Account account, decimal amount)
        {
            return Data.inSEK * Data.Currency[account.Currency];

        }
    }
}
