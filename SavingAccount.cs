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
        public decimal Intrestrate = 1.02m;
        public SavingAccount(decimal initalbalance, string Currency, decimal intrestrate) : base(initalbalance, Currency)
        {
            Intrestrate = intrestrate;
        }
    }
}
