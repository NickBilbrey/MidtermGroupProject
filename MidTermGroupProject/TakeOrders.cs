using MidTermGroupProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class TakeOrders
{
    static List<Order> orders = new List<Order>();
    static decimal salesTaxRate = 0.07m;
    static Payment payment;
    static Cash cash;
    static Check check;


    static void Main(string[] args)
    {
        Menu menu = new Menu();
        Cart cart = new Cart();
        List<Product> products = menu.Items;
        decimal totalTax = cart.GetSalesTax();
        bool done = false;
        decimal subtotal = 0;
        FileOperator fileOperator = new FileOperator();

        int cntr = 0;        
        Credit credit = new Credit();
        string choice;
        //string path = string.Empty;


       
        Console.WindowWidth = 50;
        Console.WindowHeight = 25;

        Random rand = new Random();
        fileOperator.getFile();

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

        if (cntr == 0 || cntr == 3)
        {
            Console.Clear();
            menu.DisplayProductList();
            cntr = 1;
        }



        do
        {
            

            cntr++;

            Console.WriteLine();
           
                // Get the user's choice
                Console.Write("Enter the product number you want: ");
                choice = Console.ReadLine();
           
            if (int.TryParse(choice, out int productIndex))
            {

                if (productIndex >= 0 && productIndex < products.Count) {

                    Console.WriteLine("product number is valid");
                }else
                {
                    Console.WriteLine("product number is invalid plz re-try");

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

                        Console.WriteLine("Receipt:");

                        foreach (var Order in orders)
                        {
                            Console.WriteLine("{0} {1} ...... {2} ", Order.Quantity, Order.Name, Order.LineTotal);
                        }
                        Console.WriteLine();
                        Console.WriteLine("Subtotal:    " + subtotal);
                        Console.WriteLine("Sales Tax:   " + salesTax);
                        Console.WriteLine("--------------------------");
                        Console.WriteLine("Grand Total: " + grandTotal);

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
                                    Console.WriteLine("please give the full amount you owe.");
                                    fullAmount = true;
                                }
                                else
                                {
                                    fullAmount = false;
                                }
                            } while (fullAmount == true);


                            decimal change = amountTendered - grandTotal;
                            //to do : add all the items that were ordered in receipt
                            Console.ForegroundColor = ConsoleColor.Blue;
                            //  Console.WriteLine("*******You ordered the items {0} {1}*********", quantity, products[productIndex - 1].getProductName());
                            Console.WriteLine();
                            Console.WriteLine("*********************");
                            //  Console.WriteLine("your sales tax is: "+ salesTax);
                            Console.WriteLine($"Payment Type: Cash");
                            Console.WriteLine($"Amount Tendered: ${amountTendered}");
                            Console.WriteLine($"Change: ${change}");
                            Console.WriteLine();
                            Console.WriteLine();
                            //  Console.WriteLine("your grand total is: " + grandTotal);
                            Console.ResetColor();
                        }

                        //Handle check payment
                        else if (paymentType == "CHECK")
                        {
                            try
                            {
                                //We used Regex expressions to validate the check number.if always 9 digits are required

                                Console.Write("Please enter a check number. The number should be between 1 to 9 digits long: ");
                                string pattern = @"^[1-9][0-9]{8}$";
                                string inputValue = Console.ReadLine();
                                Console.WriteLine("The number is: " + inputValue);

                                if (Regex.IsMatch(inputValue, pattern))
                                {
                                    Console.WriteLine("valid check number.");
                                }
                                // If check number is valid, continue with process
                                Console.WriteLine("Check processed successfully.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }


                            //Print the receipt                            
                            //    Console.WriteLine("Plz collect your Receipt:");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            //    Console.WriteLine("*******You ordered the items {0} {1}*********", quantity, products[productIndex - 1].getProductName());
                            Console.WriteLine();
                            Console.WriteLine("*********************");
                            //  Console.WriteLine("your sales tax is: " + salesTax);                            
                            Console.WriteLine("your grand total is: " + grandTotal);
                            Console.ResetColor();

                        }

                        else if (paymentType == "CREDIT")
                        {
                            // Get credit card number
                            Console.Write("Enter your credit card number: ");
                            string creditCardNumber = Console.ReadLine();

                            bool isCreditCardValid = Regex.IsMatch(creditCardNumber, @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]
                                 {14}|3[47][0-9]{13}|6(?:011|5[0-9]{2})[0-9]{12})$");
                            // Validate credit card number                                
                            {
                                Console.WriteLine(isCreditCardValid);

                            }

                            // Get expiration date
                            Console.Write("Enter your expiration date (MMYY): ");
                            string expirationDate = Console.ReadLine();

                            bool isExpirationDateValid = Regex.IsMatch(expirationDate, @"^(0[1-9]|1[0-2])\/([0-9]{2})$");
                            Console.WriteLine(isExpirationDateValid);

                            // Get CVV
                            Console.Write("Enter your CVV: ");
                            string cvv = Console.ReadLine();

                            bool isCVVValid = (Regex.IsMatch(cvv, @"^\d{3}$"));

                            Console.WriteLine("valid security code number.");

                            if (isCreditCardValid == true || isExpirationDateValid == true || isCVVValid == true)
                            {
                                Console.WriteLine("Payment processed successfully.");
                            }

                            //Print the receipt                            
                            // Console.WriteLine("payment was successful. Plz collect your Receipt:");                            
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
        }
        while (!done);
        Console.WriteLine("Thank you for shopping with us!");
    }

}

