using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class PendingTrans
    {
        public Account ToAccount { get; set; }
        public decimal Amount  { get; set; }
    }
}
