using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class SavingAccount : Account
    {
        internal decimal IntrestRate = 1.02m;
        internal SavingAccount(decimal initalbalance, string Currency, decimal intrestrate) : base(initalbalance, Currency)
        {
            IntrestRate = intrestrate;
        }
    }
}
