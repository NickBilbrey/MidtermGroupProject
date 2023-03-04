using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Cash : Payment
    {
        public decimal CustomerChange { get; set; } // Customer change to be returned later
        public Cash(decimal customerCash) // will subtract the Total from the Cash given and store the change
        {
            
            CustomerChange = customerCash - Total;
        }
       
        

      
    }
}
