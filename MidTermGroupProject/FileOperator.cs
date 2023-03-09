﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermGroupProject
{
    public class FileOperator : TakeOrders
    {
        List<Product> products = new Menu().Items;
        string path = string.Empty;

        public FileOperator() {}

       

        public void getFile() {

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(@"C:\Temp\menu1.txt"))
                {
                    foreach (Product prod in products)
                    {
                        sw.WriteLine($"{prod.Name},'||',{prod.Category},'||',{prod.Description},'||',{prod.Price},'||',{prod.Quantity}");

                    }

                    //using (StreamWriter writer = new StreamWriter(@"C:\Temp\menu1.txt", true))
                    //{
                    sw.WriteLine($"Danish,'||',dessert,'||',yummy,'||',1.22,'||',4");

                    //}
                    sw.Flush();
                }
            }
        }

        public void openFile() {

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(@"C:\Temp\menu1.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);

                }

            }
        }

    }
}