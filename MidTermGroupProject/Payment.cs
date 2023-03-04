using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public abstract class Payment // Abstract parent class of the payment methods
    {
        public decimal Subtotal { get; set; } // Every payment method must return both Subtotal, Sales Tax, and Total
        public decimal Total { get; set; }
        public decimal SalesTax { get; set; }
        public abstract decimal GetTotal(); // A Method to be overrided and changed based on the necessary return & Parameters

    }
    
}
