using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Check : Payment
    {
        public int CheckNumber { get; set; }
        public decimal CheckAmount { get; set; }
        public Check(int checkNumber)  // Gets the check number and then I added check amount to maybe do a try catch in case the check is not for the correct amount
        { 
            CheckNumber= checkNumber;
            
        }
        
    }
}
