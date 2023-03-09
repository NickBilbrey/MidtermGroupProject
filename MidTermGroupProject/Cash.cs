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
        public decimal AmountTendered { get; set; }
    }
}
