using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class PendingTrans
    {
        internal Account? ToAccount { get; set; }
        internal decimal Amount  { get; set; }
    }
}
