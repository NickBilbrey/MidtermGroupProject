using MidTermGroupProject;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class TakeOrders
{
    static List<Product> products = new List<Product>();
    static List<Order> orders = new List<Order>();
    static decimal salesTaxRate = 0.07m;
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
        decimal totalTax = cart.GetSalesTax();
        string path = string.Empty;

        


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

        if (!File.Exists(path))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(@"C:\Temp\sample.txt"))
            {
                foreach (Product prod in products)
                {
                    sw.WriteLine($"{prod.Name},{prod.Category},{prod.Description},{prod.Price}");
                }               

            }
        }

        // Open the file to read from.
        using (StreamReader sr = File.OpenText(@"C:\Temp\sample.txt"))
        {
            string s;
            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }
        }


        bool done = false;
        decimal subtotal = 0;
        int cntr = 0;

        Console.WindowWidth = 50;
        Console.WindowHeight = 25;

        Random rand = new Random();

        for (int i = 0; i < Console.WindowWidth; i++)
        {
            int height = rand.Next(Console.WindowHeight);
            Console.SetCursorPosition(i, height);
            Console.Write("*");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Thread.Sleep(40);
        }
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("****************** Welcome to the AJN coffee shop! Please select an item*****************:");
            Thread.Sleep(1000);
            Console.BackgroundColor = ConsoleColor.Gray;
            // cntr++;
        do
        { 
            if (cntr == 0 || cntr == 3)
            {
                Console.Clear();
                displayProductList();
                cntr = 1;
            }

            cntr++;
            
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
                      //  Console.WriteLine("testing if condition *******");
                        // Add the order to the List
                        orders.Add(new Order(products[productIndex - 1], products[productIndex - 1].Quantity));
                        cart.AddItem(products[productIndex - 1], quantity);
                        Console.WriteLine("Added {0} {1} to order", quantity, products[productIndex - 1].getProductName());

                    }
                    decimal lineTotal = products[productIndex - 1].Price * quantity;
                    subtotal = subtotal + lineTotal;

                    Console.WriteLine($"Line total: ${lineTotal}");
                    
                    // Ask the user if they want to continue shopping or complete the purchase
                    Console.Write("Continue shopping? (Y/N): ");
                    string continueShopping = Console.ReadLine().ToUpper();
                    Console.WriteLine();
                    if (continueShopping != "Y")
                    {
                        done = true;
                        decimal amountTendered;
                        decimal salesTax = Math.Round(subtotal * salesTaxRate, 2);
                        decimal grandTotal = Math.Round(subtotal + salesTaxRate, 2);

                        // Ask the user for the payment type
                        Console.Write("Enter payment type (Cash/Credit/Check): ");
                        string paymentType = Console.ReadLine().ToUpper();

                        
                       
                        // Handle cash payment
                        if (paymentType == "CASH")
                        {
                            bool fullAmount = false;
                            do
                            {

                                // Ask the user for the amount tendered
                                Console.Write("Enter amount tendered: ");
                                amountTendered = decimal.Parse(Console.ReadLine());
                                if (amountTendered < grandTotal)
                                {
                                    Console.WriteLine("plz give the full amount you owe. plz re-try");
                                    break;
                                    //fullAmount = true;
                                }
                                else
                                {
                                    continue;
                                }
                            } while (false); ;


                            decimal change = amountTendered - grandTotal;
                            //to do : add all the items that were ordered in receipt
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("*******You ordered the items {0} {1}*********", quantity, products[productIndex - 1].getProductName());
                            Console.WriteLine();
                            Console.WriteLine("*********************");                            
                            Console.WriteLine("your sales tax is: "+ salesTax);
                            Console.WriteLine($"Payment Type: Cash");
                            Console.WriteLine($"Amount Tendered: ${amountTendered}");
                            Console.WriteLine($"Change: ${change}");
                            Console.WriteLine("your grand total is: " + grandTotal);
                            Console.ResetColor();
                        }
                        
                        //Handle check payment
                        else if (paymentType == "CHECK")
                        {
                            //We used Regex expressions to validate the check number.if always 9 digits are required
                            string pattern = @"^[1-9][0-9]{8}$";
                            Console.Write("Please enter a check number. The number should be between 1 to 9 digits long: ");
                            string inputValue = Console.ReadLine();
                            Console.WriteLine("The number is: " + inputValue);
                            
                            if (Regex.IsMatch(inputValue, pattern))
                            {
                                Console.WriteLine($"Input text {inputValue} matches.");
                            }
                            else
                            {
                                Console.WriteLine($"Input text {inputValue} does not match.plz re-enter the check number: ");
                                string inputValue1 = Console.ReadLine();
                            }                            
                           
                            //Print the receipt                            
                            Console.WriteLine("Plz collect your Receipt:");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("*******You ordered the items {0} {1}*********", quantity, products[productIndex - 1].getProductName());
                            Console.WriteLine();
                            Console.WriteLine("*********************");
                            Console.WriteLine("your sales tax is: " + salesTax);                            
                            Console.WriteLine("your grand total is: " + grandTotal);
                            Console.ResetColor();

                        }
                    
                        else if (paymentType == "CREDIT")
                        {
                            Console.WriteLine("Enter credit card number:");
                            string cardNumber = Console.ReadLine();
                            Console.WriteLine("Enter expiration date (MM/YY):");
                            string expirationDate = Console.ReadLine();
                            Console.WriteLine("Enter CVV:");
                            string cvv = Console.ReadLine();

                            //Print the receipt                            
                            Console.WriteLine("payment was successful. Plz collect your Receipt:");                            
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("*******You ordered the items {0} {1}*********", quantity, products[productIndex - 1].getProductName());
                            Console.WriteLine();
                            Console.WriteLine("*********************");
                            Console.WriteLine("your sales tax is: " + salesTax);
                            Console.WriteLine("your grand total is: " + grandTotal);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("Invalid payment type.");
                        }



                    }
                }
            }
        } while (!done);
        Console.WriteLine("Thank you for shopping with us!");;
    }
    }

