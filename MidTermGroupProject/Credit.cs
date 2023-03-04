using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Credit : Payment
    {
        public string CreditCardNumber { get; set; }
        public string ExperationDate { get; set; }
        public int CVV { get; set; }
        public Credit(string creditCardNumber, string experationDate, int cVV) // constructs and sets all the credit card info to be returned later
        {
            CreditCardNumber= creditCardNumber;
            ExperationDate= experationDate;
            CVV= cVV;
        }
       
    }
}
