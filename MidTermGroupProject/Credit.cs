using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Credit : Payment
    {
        public long CreditCardNumber { get; set; }
        public int ExperationDate { get; set; }
        public int CVV { get; set; }
        public Credit(long creditCardNumber, int experationDate, int cVV) // constructs and sets all the credit card info to be returned later
        {
            CreditCardNumber= creditCardNumber;
            ExperationDate= experationDate;
            CVV= cVV;
        }
       
    }
}
