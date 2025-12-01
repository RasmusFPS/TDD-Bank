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
        internal void WithdrawExchange(Account account, decimal amount)
        {
            Data.inSEK = amount / Data.Currency[account.Currency];
        }
        internal decimal DepositExchange(Account account, decimal amount)
        {
            return Data.inSEK * Data.Currency[account.Currency];

        }
    }
}
