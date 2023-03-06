using MidTermGroupProject;
using System.Security.Cryptography.X509Certificates;

public class TakeOrders
{
    static List<Product> products = new List<Product>();
    static List<Order> orders = new List<Order>();
    static Cart cart = new Cart();
    static decimal salesTax;
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
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int productIndex))
            {
                if (productIndex >= 1 && productIndex <= products.Count)
                {
                    // Get the quantity
                    Console.Write("Enter quantity: ");
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
                        // Calculate the subtotal
                        
                        decimal salesTax = Math.Round(subtotal * salesTaxRate, 2);

                        // Calculate the grand total
                        decimal grandTotal = Math.Round(subtotal + salesTax, 2);
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
                            // Ask the user for the amount tendered
                            Console.Write("Enter amount tendered: ");
                            decimal amountTendered = decimal.Parse(Console.ReadLine());

                            // Calculate the change
                            decimal change = amountTendered - grandTotal;

                            //Print the receipt
                        //    Console.WriteLine("Receipt:");
                        //    for (int i = 0; i < products.Count; i++)
                        //    {
                        //        Console.WriteLine($"{products[i].Name} - ${products[i].Price}");
                        //    }
                            Console.ForegroundColor = ConsoleColor.Blue;
                            //   Console.WriteLine($"Subtotal: ${subtotal}");
                            //  Console.WriteLine($"Sales Tax: ${salesTax}");
                            //  Console.WriteLine($"Grand Total: ${grandTotal}");
                            //  Console.WriteLine($"Payment Type: Cash");
                            //  Console.WriteLine($"Amount Tendered: ${amountTendered}");
                            //  Console.WriteLine($"Change: ${change}");
                            Console.WriteLine();
                            Console.WriteLine("*********************");
                            Console.WriteLine($"Payment Type: Cash");
                            Console.WriteLine($"Amount Tendered: ${amountTendered}");
                            Console.WriteLine($"Change: ${change}");


                            Console.ResetColor();
                        }
                        //Handle check payment
                        else if (paymentType == "CHECK")
                        {
                            //Ask the user for the check number

                            Console.Write("Enter check number: ");
                            string checkNumber = Console.ReadLine();

                            // Print the receipt

                        }
                    /*    else if (paymentType == "check")
                        {
                            Console.WriteLine("Enter check number:");
                            string checkNumber = Console.ReadLine();
                        }
                    */    else if (paymentType == "CREDIT")
                        {
                            Console.WriteLine("Enter credit card number:");
                            string cardNumber = Console.ReadLine();
                            Console.WriteLine("Enter expiration date (MM/YY):");
                            string expirationDate = Console.ReadLine();
                            Console.WriteLine("Enter CVV:");
                            string cvv = Console.ReadLine();
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