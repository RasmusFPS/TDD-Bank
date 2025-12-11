using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class TransferLog
    {
        internal int FromAccount { get; set; }
        internal int ToAccount { get; set; }
        internal decimal Amount { get; set; }
        internal string? Currency { get; set; }
        internal string? FromUser { get; set; }
        internal string? ToUser { get; set; }
        internal DateTime LogTime { get; set; }
    }
}
