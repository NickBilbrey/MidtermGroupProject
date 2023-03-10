using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class Menu
    {
        public List<Product> Items = new List<Product>();

        public Menu()
        {
            string path = string.Empty;

            // Populate the product list
            Items.Add(new Product("Coffee", "Beverage", "Regular coffee", 2.00m, 0));
            Items.Add(new Product("Latte", "Beverage", "Espresso with steamed milk", 3.50m, 0));
            Items.Add(new Product("Cappuccino", "Beverage", "Espresso with frothed milk", 3.50m, 0));
            Items.Add(new Product("Muffin", "Pastry", "Blueberry muffin", 2.50m, 0));
            Items.Add(new Product("Croissant", "Pastry", "Buttery croissant", 2.00m, 0));
            Items.Add(new Product("Bagel", "Pastry", "Plain bagel with cream cheese", 2.50m, 0));
            Items.Add(new Product("Breakfast Sandwich", "Food", "Egg and cheese on a croissant", 4.50m, 0));
            Items.Add(new Product("BLT Sandwich", "Food", "Bacon, lettuce, and tomato on toast", 5.50m, 0));
            Items.Add(new Product("Soup of the Day", "Food", "Chef's special soup", 4.00m, 0));
            Items.Add(new Product("Salad", "Food", "Mixed greens with balsamic vinaigrette", 5.00m, 0));
            Items.Add(new Product("Fruit Cup", "Food", "Assorted seasonal fruit", 3.50m, 0));
            Items.Add(new Product("Yogurt Parfait", "Food", "Greek yogurt with granola and berries", 4.50m, 0));

        }
        public void DisplayProductList()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Console.WriteLine("{0}. {1} ({2}) - {3:C}", i + 1, Items[i].Name, Items[i].Category, Items[i].Price);
            }
                     
        }



    }
}
