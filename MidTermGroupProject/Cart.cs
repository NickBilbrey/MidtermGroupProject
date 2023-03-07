using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Cart
    {
        public List<Product> Items = new List<Product>();

        public void AddItem(Product product, int quantity)
        {
            Items.Add(product);            
        }
        
        public decimal GetSalesTax()
        {           

           decimal taxRate = 0.06m;           

            return taxRate;
        }
       
        public void Clear()
        {
            Items.Clear();
        }
    }
}
