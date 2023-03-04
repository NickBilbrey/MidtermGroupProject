using MidTermGroupProject;

public class TakeOrders
{
    static List<Product> products = new List<Product>();
    static List<Order> orders = new List<Order>();
    static decimal salesTaxRate = 0.07m;

    static void Main(string[] args)
    {
        // Populate the product list
        products.Add(new Product("Coffee", "Beverage", "Regular coffee", 2.00m));
        products.Add(new Product("Latte", "Beverage", "Espresso with steamed milk", 3.50m));
        products.Add(new Product("Cappuccino", "Beverage", "Espresso with frothed milk", 3.50m));
        products.Add(new Product("Muffin", "Pastry", "Blueberry muffin", 2.50m));
        products.Add(new Product("Croissant", "Pastry", "Buttery croissant", 2.00m));
        products.Add(new Product("Bagel", "Pastry", "Plain bagel with cream cheese", 2.50m));
        products.Add(new Product("Breakfast Sandwich", "Food", "Egg and cheese on a croissant", 4.50m));
        products.Add(new Product("BLT Sandwich", "Food", "Bacon, lettuce, and tomato on toast", 5.50m));
        products.Add(new Product("Soup of the Day", "Food", "Chef's special soup", 4.00m));
        products.Add(new Product("Salad", "Food", "Mixed greens with balsamic vinaigrette", 5.00m));
        products.Add(new Product("Fruit Cup", "Food", "Assorted seasonal fruit", 3.50m));
        products.Add(new Product("Yogurt Parfait", "Food", "Greek yogurt with granola and berries", 4.50m));

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the coffee shop! Please select an item:");

            // Display the product list
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine("{0}. {1} ({2}) - {3:C}", i + 1, products[i].Name, products[i].Category, products[i].Price);
            }

            Console.WriteLine("{0}. Complete order", products.Count + 1);
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
                        // Add the order to the List
                        orders.Add(new Order(products[productIndex - 1], quantity));
                        Console.WriteLine("Added {0} x {1} to order", quantity, products);
                    }
                    // Calculate the line total check ths
                    decimal lineTotal = products[productIndex - 1].Price * quantity;

                    // Print the line total
                    Console.WriteLine($"Line total: ${lineTotal}");

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

                        // Calculate the sales tax
                        decimal salesTax = Math.Round(subtotal * salesTaxRate, 2);

                        // Calculate the grand total
                        decimal grandTotal = Math.Round(subtotal + salesTax, 2);

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

                            // Print the receipt
                            Console.WriteLine("Receipt:");
                            for (int i = 0; i < products.Count; i++)
                            {
                                Console.WriteLine($"{products[i].Name} - ${products[i].Price}");
                            }
                            Console.WriteLine($"Subtotal: ${subtotal}");
                            Console.WriteLine($"Sales Tax: ${salesTax}");
                            Console.WriteLine($"Grand Total: ${grandTotal}");
                            Console.WriteLine($"Payment Type: Cash");
                            Console.WriteLine($"Amount Tendered: ${amountTendered}");
                            Console.WriteLine($"Change: ${change}");
                        }
                        // Handle check payment
                        else if (paymentType == "CHECK")
                        {
                            // Ask the user for the check number
                            Console.Write("Enter check number: ");
                            string checkNumber = Console.ReadLine();

                            // Print the receipt

                        }
                        else if (paymentType == "check")
                        {
                            Console.WriteLine("Enter check number:");
                            string checkNumber = Console.ReadLine();
                        }
                        else if (paymentType == "credit")
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

                        Console.WriteLine("Thank you for shopping with us!");
                    }
                }
            }
        }
    }
}