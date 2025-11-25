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
        //public decimal startValue;

        //internal void ExchangeStart()
        //{
        //    if (account.Currency != "SEK")
        //    {
        //        startValue = account.Balance;
        //        Exchange.ValueExchangeSek(account, sek, startValue, amount, addToAccount);
        //    }
        //    else if (account.Currency == "SEK")
        //    {

        //    }
        //}
        public decimal sek;
    public static decimal amountOne;
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
        //Account account, decimal sek, decimal startValue, decimal amount, bool addToAccount
        internal decimal ValueExchangeOne(Account account, decimal amount, bool addToAccount)
        {
            decimal amountTwo = amount;

            decimal startValue = account.Balance;
            if (account.Currency == "USD")
            {
                amount = amount / 0.105m;
                amountOne = amount;
                startValue = startValue / 0.105m;
                //if (addToAccount == false)
                //{
                //    startValue = -amount;
                //    startValue *= 0.105m;
                //    //decimal amountTwo = amount;
                //    //return amountTwo;
                //}
                
                //sek = account.Balance / 0.105m;
            }
            else if (account.Currency == "EUR")
            {
                amount = amount / 0.091m;
                amountOne = amount;
                startValue = startValue / 0.091m;
                //if (addToAccount == false)
                //{
                //    startValue = -amount;
                //    startValue *= 0.091m;
                //}

                //sek = account.Balance / 0.091m;
            }
            return amountTwo;
        }
            internal decimal ValueExchangeTwo(Account account, bool addToAccount)
        {
            decimal startValue = account.Balance;
            if (account.Currency == "USD")
            {
                amountOne *= 0.105m;
                //startValue = startValue / 0.105m;
                //if (addToAccount == true)
                //{
                //    startValue = +amount;
                //    startValue *= 0.105m;
                //}



                //sek = account.Balance / 0.105m;
            }
            else if (account.Currency == "EUR")
            {
                amountOne = amountOne * 0.091m;
                //startValue = startValue / 0.091m;
                //if (addToAccount == true)
                //{
                //    startValue = +amount;
                //    startValue *= 0.091m;
                //}
                

                //sek = account.Balance / 0.091m;
            }
                    //decimal amountOne = amount;
                    return amountOne;
        }
        
            //else if (account.Currency == "DKK")
            //{
            //    sek = account.Balance / 0.68m;
            //}
        

    }
}
