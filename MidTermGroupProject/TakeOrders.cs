﻿using MidTermGroupProject;
using System.Security.Cryptography.X509Certificates;

public class TakeOrders
{
    static List<Product> products = new List<Product>();
    static List<Order> orders = new List<Order>();
    static Cart cart = new Cart();
    static decimal salesTax;
    static Payment payment;
    static Cash cash;
    static Check check;
    static Credit credit;

    public static void displayProductList()
    {
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine("{0}. {1} ({2}) - {3:C}", i + 1, products[i].Name, products[i].Category, products[i].Price);
        }

    }
    
    static void Main(string[] args)
    {
        Cart cart = new Cart();
        decimal totalSalesTax = cart.GetSalesTax();

        // Populate the product list
        products.Add(new Product("Coffee", "Beverage", "Regular coffee", 2.00m, 0));
        products.Add(new Product("Latte", "Beverage", "Espresso with steamed milk", 3.50m, 0));
        products.Add(new Product("Cappuccino", "Beverage", "Espresso with frothed milk", 3.50m, 0));
        products.Add(new Product("Muffin", "Pastry", "Blueberry muffin", 2.50m, 0));
        products.Add(new Product("Croissant", "Pastry", "Buttery croissant", 2.00m, 0));
        products.Add(new Product("Bagel", "Pastry", "Plain bagel with cream cheese", 2.50m, 0));
        products.Add(new Product("Breakfast Sandwich", "Food", "Egg and cheese on a croissant", 4.50m, 0));
        products.Add(new Product("BLT Sandwich", "Food", "Bacon, lettuce, and tomato on toast", 5.50m, 0));
        products.Add(new Product("Soup of the Day", "Food", "Chef's special soup", 4.00m, 0));
        products.Add(new Product("Salad", "Food", "Mixed greens with balsamic vinaigrette", 5.00m, 0));
        products.Add(new Product("Fruit Cup", "Food", "Assorted seasonal fruit", 3.50m, 0));
        products.Add(new Product("Yogurt Parfait", "Food", "Greek yogurt with granola and berries", 4.50m, 0));

       
        Console.WindowWidth = 50;
        Console.WindowHeight = 25;

        Random rand = new Random();

        do
        {

       
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                int height = rand.Next(Console.WindowHeight);
                Console.SetCursorPosition(i, height);
                Console.Write("*");
                Console.ForegroundColor = ConsoleColor.Green;
               // Console.BackgroundColor = ConsoleColor.Black;
                Thread.Sleep(40);
            }

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("****************** Welcome to the AJN coffee shop! Please select an item*****************:");
            Thread.Sleep(1000);
            Console.BackgroundColor = ConsoleColor.Gray;


            displayProductList();

            Console.WriteLine("{0}. Complete order", products.Count + 1);
            Console.WriteLine();

            // Get the user's choice
            Console.Write("Enter the product number you want: ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int productIndex))
            {
                if (productIndex >= 1 && productIndex <= products.Count)
                {
                    // Get the quantity
                    Console.Write("How much is the quantity of the product you want ");
                    string quantityString = Console.ReadLine();
                    if (int.TryParse(quantityString, out int quantity) && quantity > 0)
                    {
                        products[productIndex - 1].Quantity = quantity;                       
                        // Add the order to the List
                        orders.Add(new Order(products[productIndex - 1], products[productIndex - 1].Quantity));
                        cart.AddItem(products[productIndex - 1], quantity);
                        Console.WriteLine("Added {0} {1} to order", quantity, products[productIndex - 1].getProductName());

                    }

                    static void ViewProducts()
                    {
                        Console.WriteLine("Products:");
                        for (int i = 0; i < products.Count; i++)
                        {
                            Console.WriteLine("{0}. {1} - {2} ({3:c})", i + 1, products[i].Name, products[i].Category, products[i].Price);
                        }
                    }

                    // Ask the user if they want to continue shopping or complete the purchase
                    Console.Write("Continue shopping? (Y/N): ");
                    string continueShopping = Console.ReadLine().ToUpper();
                    if (continueShopping == "N")
                    {
                        // Calculate the subtotal
                        decimal subtotal = 0;
                        for (int i = 0; i < products.Count; i++)
                        {
                            subtotal += products[i].Price;
                        }
                        Console.WriteLine("1. Cash");
                        Console.WriteLine("2. Credit");
                        Console.WriteLine("3. Check");
                        // Calculate the line total check ths
                        decimal lineTotal = products[productIndex - 1].Price * quantity;

                        // Print the line total

                        Console.WriteLine($"Line total: ${lineTotal}");
                        decimal grandTotal = lineTotal + totalSalesTax;
                        Console.WriteLine($"grand total: ${grandTotal}");

                        // Ask the user for the payment type
                        Console.Write("Enter payment type (Cash/Credit/Check): ");
                        string paymentType = Console.ReadLine().ToUpper();

                        // Handle cash payment
                        if (paymentType == "CASH")
                        {
                            // Ask the user for the amount tendered
                            Console.Write("Enter amount tendered: ");
                            decimal amountTendered = decimal.Parse(Console.ReadLine());                           
                            
                            // Calculate the change
                            decimal change = amountTendered - grandTotal;
                            Console.WriteLine($"Change: ${change}");
                            Console.WriteLine($"Sales Tax: ${totalSalesTax}");
                            Console.WriteLine($"Grand Total: ${grandTotal}");
                            Console.WriteLine($"Payment Type: Cash");
                            Console.WriteLine($"Amount Tendered: ${amountTendered}");
                            Console.WriteLine(" plz collect your change: "+ change);

                            //Print the receipt                            
                            Console.WriteLine("Plz collect your Receipt:" );                 
                                                       
                        }
                        //Handle check payment
                        else if (paymentType == "CHECK")
                        {
                            //Ask the user for the check number

                            Console.Write("Enter check number: ");
                            string checkNumber = Console.ReadLine();

                            //Print the receipt                            
                            Console.WriteLine("Plz collect your Receipt:");

                        }
                        
                        else if (paymentType == "credit")
                        {
                            Console.WriteLine("Enter credit card number:");
                            string cardNumber = Console.ReadLine();
                            Console.WriteLine("Enter expiration date (MM/YY):");
                            string expirationDate = Console.ReadLine();
                            Console.WriteLine("Enter CVV:");
                            string cvv = Console.ReadLine();

                            //Print the receipt                            
                            Console.WriteLine("payment was successful. Plz collect your Receipt:");
                        }
                        else
                        {
                            Console.WriteLine("Invalid payment type.plz try again");
                        }
                       // Console.BackgroundColor = ConsoleColor.Green;   
                        Console.WriteLine("Thank you for shopping with us. see you again soon!");
                    }
                }
            }
        } while (false);
    }
    }
