using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Online Ordering Program ===\n");

        // Create addresses
        Address address1 = new Address("123 Main St", "Springfield", "IL", "USA");
        Address address2 = new Address("456 Queen St", "Toronto", "ON", "Canada");
        Address address3 = new Address("789 Maple Ave", "Austin", "TX", "USA");
        Address address4 = new Address("321 Oak Rd", "London", "Greater London", "United Kingdom");

        // Create customers
        Customer customer1 = new Customer("John Smith", address1);
        Customer customer2 = new Customer("Sarah Johnson", address2);
        Customer customer3 = new Customer("Michael Brown", address3);
        Customer customer4 = new Customer("Emily Davis", address4);

        // Create orders
        Order order1 = new Order(customer1);
        Order order2 = new Order(customer2);
        Order order3 = new Order(customer3);
        Order order4 = new Order(customer4);

        // Add products to order 1
        order1.AddProduct(new Product("Laptop", "TECH-001", 799.99, 1));
        order1.AddProduct(new Product("Wireless Mouse", "TECH-002", 29.99, 2));
        order1.AddProduct(new Product("USB Cable", "TECH-003", 9.99, 3));

        // Add products to order 2
        order2.AddProduct(new Product("Coffee Maker", "KIT-001", 149.50, 1));
        order2.AddProduct(new Product("Coffee Beans (1kg)", "KIT-002", 24.99, 2));
        order2.AddProduct(new Product("Travel Mug", "KIT-003", 12.99, 1));

        // Add products to order 3
        order3.AddProduct(new Product("Smartphone", "TECH-004", 699.00, 1));
        order3.AddProduct(new Product("Phone Case", "TECH-005", 19.99, 2));
        order3.AddProduct(new Product("Screen Protector", "TECH-006", 12.99, 3));

        // Add products to order 4
        order4.AddProduct(new Product("Book: Clean Code", "BOOK-001", 39.99, 1));
        order4.AddProduct(new Product("Book: Design Patterns", "BOOK-002", 44.99, 1));
        order4.AddProduct(new Product("Bookmark Set", "BOOK-003", 5.99, 2));

        // Create list of orders
        Order[] orders = { order1, order2, order3, order4 };

        // Display all orders
        foreach (Order order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine();
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine();
            Console.WriteLine($"Total Price: ${order.GetTotalCost():F2}");
            Console.WriteLine("========================================\n");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
