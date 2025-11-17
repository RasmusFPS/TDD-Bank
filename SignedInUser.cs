using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Bank
{
    internal class SignedInUser
    {
        double Balance;
        public void Deposite()
        {
            Console.WriteLine("How much money do you want to Deposite!");
            double input = Convert.ToDouble(Console.ReadLine());
            Balance += input;
        }
    }
}
