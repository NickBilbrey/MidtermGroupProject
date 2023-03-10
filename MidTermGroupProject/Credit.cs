using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Credit : Payment
    {
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }

        public bool IsCardNumberValid()
        {
            if (Regex.IsMatch(CreditCardNumber, @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|3[47][0-9]{13}|6(?:011|5[0-9]{2})[0-9]{12})$"))
            {
                return true;
            }
            return false;
        }
        public bool IsExpirationDateValid()
        {
            if (Regex.IsMatch(ExpirationDate, @"^(0[1-9]|1[0-2])\/([0-9]{2})$"))
            {
                return true;
            }
            return false;
        }
        public bool IsCvvValid()
        {
            if (Regex.IsMatch(CVV, @"^\d{3}$"))
            {
                return true;
            }
            return false;
        }



    }
}
