using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Check : Payment
    {
        public long CheckNumber { get; set; }
        public decimal CheckAmount { get; set; }
        public Check(long checkNumber)  // Gets the check number and then I added check amount to maybe do a try catch in case the check is not for the correct amount
        { 
            CheckNumber= checkNumber;
            
        }
        
    }
}
