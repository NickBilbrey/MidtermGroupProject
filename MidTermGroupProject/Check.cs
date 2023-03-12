using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Check : Payment
    {
        public string CheckNumber { get; set; }
        public string Pattern = @"^[1-9][0-9]{8}$";

        public bool ValidCheck()    //We used Regex expressions to validate the check number.if always 9 digits are required
        {
            if (Regex.IsMatch(CheckNumber, Pattern))
            {
                return true;
            }
            return false;


        }

    }
}
