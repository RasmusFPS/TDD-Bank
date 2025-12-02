using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class TransferLog
    {
        //Ev en dataTime här.
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }

        public DateTime LogTime { get; set; }
    }
}
