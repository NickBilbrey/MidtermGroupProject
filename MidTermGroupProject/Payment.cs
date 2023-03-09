using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Payment // Abstract parent class of the payment methods
    {
        public decimal Subtotal { get; set; } // Every payment method must return both Subtotal, Sales Tax, and Total
        public decimal LineTotal { get; set; }
        public decimal SalesTax { get; set; }
        public decimal SalesTaxTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public Payment() 
        {
            SalesTax= 0.07m;
        }

    }
    
}
