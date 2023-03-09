using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product (string name, string category, string description, decimal price, int quantity)
        {
            Name = name;
            Category = category;
            Description = description;
            Price = price;
            Quantity = quantity;    
        }

        public string getProductName() {
            return Name;
        }

        public string getProductCategory()
        {
            return Category;
        }

       


    }
}
