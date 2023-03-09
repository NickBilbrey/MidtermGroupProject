using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Cart:Menu
    {
        public List<Order> Orders = new List<Order>();

        public void Clear()
        {
            Items.Clear();
        }
        public void AddItem(Product product, int quantity)
        {
            Items.Add(product);
        }
    }
}
