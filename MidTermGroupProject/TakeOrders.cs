using MidTermGroupProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
public class TakeOrders
{
    static void Main(string[] args)
    {
        string makeNewOrder = "";

        do
        { 
            Menu menu = new Menu();
            Cart cart = new Cart();
            Payment payment = new Payment();
            List<Product> products = menu.Items;            // Instantiates all classes and lists needed to function
            List<Order> orders = new List<Order>();
            FileOperator fileOperator = new FileOperator();
            Credit credit = new Credit();
            Cash cash = new Cash();
            Check check = new Check();
            string choice;
            bool done = false;
            int cntr = 0;

            Console.WindowWidth = 75;      
            Console.WindowHeight = 25;
            Random rand = new Random();
            fileOperator.getFile();

            using (StreamReader sr = File.OpenText(@"C:\Temp\menu1.txt"))       // Displays menu from text file
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);

                }

            }

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
                    for (int i = 0; i < products.Count; i++)
                    {
                        Console.WriteLine("{0}. {1} ({2}) - {3:C}", i + 1, products[i].Name, products[i].Category, products[i].Price);
                    }
                    cntr = 1;
                }
                cntr++;
                Console.WriteLine();
                // Get the user's choice
                Console.Write("Enter the product number you want: ");
                choice = Console.ReadLine();
                if (int.TryParse(choice, out int productIndex))
                {
                    if (productIndex > products.Count || productIndex <= 0)
                    {

                        Console.WriteLine("Menu item not available please try again");

                    }
                    if (productIndex >= 1 && productIndex <= products.Count)
                    {
                        // Get the quantity
                        Console.Write("How many " + products[productIndex - 1].getProductName() + " would you like? ");
                        string quantityString = Console.ReadLine();
                        if (int.TryParse(quantityString, out int quantity) && quantity > 0)
                        {

                            products[productIndex - 1].Quantity = quantity;
                            // Add the order to the List
                            orders.Add(new Order(products[productIndex - 1], products[productIndex - 1].Quantity, products[productIndex - 1].getProductName()));
                            cart.AddItem(products[productIndex - 1], quantity);
                            Console.WriteLine("Added {0} {1} to order", quantity, products[productIndex - 1].getProductName());


                            payment.LineTotal = products[productIndex - 1].Price * quantity;
                            payment.Subtotal = payment.Subtotal + payment.LineTotal;

                            Console.WriteLine($"Line total: ${payment.LineTotal}");
                        }
                        else
                        { 
                            Console.WriteLine("Sorry you entered an invalid quantity"); 
                        
                        }
                        // Ask the user if they want to continue shopping or complete the purchase
                        Console.Write("Continue shopping? (Y/N): ");
                        string continueShopping = Console.ReadLine().ToUpper();
                        Console.WriteLine();

                        // Begining the Receipt
                        if (continueShopping != "Y")
                        {
                            done = true;
                            payment.SalesTaxTotal = Math.Round(payment.Subtotal * payment.SalesTax, 2);
                            payment.GrandTotal = Math.Round(payment.Subtotal + payment.SalesTaxTotal, 2);
                            Console.WriteLine("Receipt:");
                            foreach (var Order in orders)
                            {
                                Console.WriteLine("{0} {1} ...... {2} ", Order.Quantity, Order.Name, Order.LineTotal);
                            }
                            Console.WriteLine();
                            Console.WriteLine("Subtotal:    " + payment.Subtotal);
                            Console.WriteLine("Sales Tax:   " + payment.SalesTaxTotal);
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("Grand Total: " + payment.GrandTotal);

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
                                    cash.AmountTendered = decimal.Parse(Console.ReadLine());

                                    if (cash.AmountTendered < payment.GrandTotal)
                                    {
                                        Console.WriteLine("please give the full amount you owe.");
                                        fullAmount = true;
                                    }
                                    else
                                    {
                                        cash.CustomerChange = cash.AmountTendered - payment.GrandTotal;
                                        fullAmount = false;
                                    }
                                } while (fullAmount == true);
                                //to do : add all the items that were ordered in receipt
                                Console.ForegroundColor = ConsoleColor.Blue;
                                //  Console.WriteLine("*******You ordered the items {0} {1}*********", quantity, products[productIndex - 1].getProductName());
                                Console.WriteLine();
                                Console.WriteLine("*********************");
                                //  Console.WriteLine("your sales tax is: "+ salesTax);
                                Console.WriteLine($"Payment Type: Cash");
                                Console.WriteLine($"Amount Tendered: ${cash.AmountTendered}");
                                Console.WriteLine($"Change: ${cash.CustomerChange}");
                                Console.WriteLine();
                                Console.WriteLine();
                                //  Console.WriteLine("your grand total is: " + grandTotal);
                                Console.ResetColor();
                            }
                            //Handle check payment
                            else if (paymentType == "CHECK")
                            {
                                do
                                {
                                    Console.Write("Please enter a check number. The number should be 9 digits long: ");
                                    check.CheckNumber = Console.ReadLine();
                                    Console.WriteLine("The number is: " + check.CheckNumber);
                                    if (check.ValidCheck() == true)
                                    {
                                        Console.WriteLine("Check processed successfully."); // If check number is valid, continue with process
                                    }
                                    else if (check.ValidCheck() == false)
                                    {
                                        Console.WriteLine("Please enter a valid check number");
                                    }
                                } while (check.ValidCheck() == false);
                                //Print the receipt
                                //    Console.WriteLine("Plz collect your Receipt:");
                                Console.ForegroundColor = ConsoleColor.Blue;
                                //    Console.WriteLine("*******You ordered the items {0} {1}*********", quantity, products[productIndex - 1].getProductName());
                                Console.WriteLine();
                                Console.WriteLine("*********************");
                                //  Console.WriteLine("your sales tax is: " + salesTax);
                                Console.WriteLine("your grand total is: " + payment.GrandTotal);
                                Console.ResetColor();
                            }

                            else if (paymentType == "CREDIT")
                            {
                                bool validEntry = false;
                                do
                                {
                                    // Get credit card number
                                    Console.Write("Enter your credit card number: ");
                                    credit.CreditCardNumber = Console.ReadLine();

                                    Console.Write("Enter your expiration date (MMYY): ");
                                    credit.ExpirationDate = Console.ReadLine();

                                    Console.Write("Enter your CVV: ");
                                    credit.CVV = Console.ReadLine();

                                    if (credit.IsCardNumberValid() == true || credit.IsExpirationDateValid() == true || credit.IsCvvValid() == true)
                                    {
                                        Console.WriteLine("Payment processed successfully.");
                                        validEntry = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter valid Credit Card Info");
                                    }
                                } while (validEntry == false);

                                //Print the receipt                            
                                // Console.WriteLine("payment was successful. Plz collect your Receipt:");                            
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("*******You ordered the items {0} {1}*********", quantity, products[productIndex - 1].getProductName());
                                Console.WriteLine();
                                Console.WriteLine("*********************");
                                Console.WriteLine("your sales tax is: " + payment.SalesTaxTotal);
                                Console.WriteLine("your grand total is: " + payment.GrandTotal);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine("Invalid payment type.");
                            }
                        }
                    }
                }
            }
            while (!done);

            Console.WriteLine("Thank you for shopping with us!");

            Console.WriteLine("would you like to make a new order? (Y/N)");

            makeNewOrder = Console.ReadLine().ToUpper();

        } while (makeNewOrder == "Y");

        Menu menu2 = new Menu();
        FileOperator fileOperator1 = new FileOperator();
        List<Product> products1 = menu2.Items;

        Console.WriteLine();
        Console.WriteLine("Do you wish to add items to the menu? (Y/N) ");
        string addItems = Console.ReadLine().ToUpper();
        if (addItems == "Y")
        {
            Console.WriteLine("Add a product: ");
            string newProduct = Console.ReadLine();

            Console.WriteLine("Is this a 'Food' 'Beverage' or 'Pastry'? ");
            string newCategory = Console.ReadLine();

            Console.WriteLine("Describe this product: ");
            string newDescription = Console.ReadLine();

            Console.WriteLine("Price for this product? ");
            string newPriceString = Console.ReadLine();

            decimal newPrice = Convert.ToDecimal(newPriceString);
            menu2.Items.Add(new Product(newProduct, newCategory, newDescription, newPrice, 0));
            fileOperator1.addToFile(newProduct, newCategory, newDescription, newPrice, 0);

            for (int i = 0; i < products1.Count; i++)
            {
                Console.WriteLine("{0}. {1} ({2}) - {3:C}", i + 1, products1[i].Name, products1[i].Category, products1[i].Price);
            }

        }
        else { Console.WriteLine("Have a good night!"); }
    }
}