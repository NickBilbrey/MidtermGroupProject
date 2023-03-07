using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    //Create the class Order
    public class Order
    {       
            public Product Product;
            public int Quantity;
            public string Name;
            public decimal LineTotal
            {
            //This will return the price of the product multiplyin
                get { return Product.Price * Quantity; }
            }
          
            public Order(Product product, int quantity, string name)
            {
                Product = product;
                Quantity = quantity;
                Name = name;
            }
        }
    }
