using System;
using System.Collections.Generic;

class Product
{
    public string PCode { get; private set; }
    public string PName { get; set; }
    public int QtyInStock { get; set; }
    public double Price { get; set; }
    public static string Brand { get; set; } = "BrandName";
    public double DiscountAllowed { get; set; }

    public Product(string pCode, string pName, int qtyInStock, double price, double discountAllowed)
    {
        PCode = pCode;
        PName = pName;
        QtyInStock = qtyInStock;
        Price = price;
        DiscountAllowed = discountAllowed;
    }

    public void DisplayProductDetails()
    {
        Console.WriteLine($"PCode: {PCode}, PName: {PName}, QtyInStock: {QtyInStock}, Price: {Price:C}, DiscountAllowed: {DiscountAllowed * 100}%");
    }
}

class Program
{
    static List<Product> products = new List<Product>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Who are you?");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Customer");
            Console.Write("Choose an option: ");
            int userType = int.Parse(Console.ReadLine());

            if (userType == 1)
            {
                AdminMenu();
            }
            else if (userType == 2)
            {
                CustomerMenu();
            }
            else
            {
                Console.WriteLine("Invalid option. Try again.");
            }
        }
    }

    static void AdminMenu()
    {
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. Display Products");
        Console.Write("Choose an option: ");
        int adminChoice = int.Parse(Console.ReadLine());

        if (adminChoice == 1)
        {
            AddProduct();
        }
        else if (adminChoice == 2)
        {
            DisplayProducts();
        }
        else
        {
            Console.WriteLine("Invalid option. Try again.");
        }
    }

    static void CustomerMenu()
    {
        Console.Write("Enter product name: ");
        string pname = Console.ReadLine();
        Product product = products.Find(p => p.PName.Equals(pname, StringComparison.OrdinalIgnoreCase));

        if (product != null)
        {
            Console.Write("Enter quantity: ");
            int qty = int.Parse(Console.ReadLine());

            if (qty <= product.QtyInStock)
            {
                double totalAmount = CalculateTotalAmount(product.Price, qty, product.DiscountAllowed);
                Console.WriteLine("Bill Details:");
                product.DisplayProductDetails();
                Console.WriteLine($"Quantity: {qty}");
                Console.WriteLine($"Total Amount (after discount): {totalAmount:C}");
                product.QtyInStock -= qty;
            }
            else
            {
                Console.WriteLine("Insufficient stock.");
            }
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    static void AddProduct()
    {
        Console.Write("Enter Product Code: ");
        string pCode = Console.ReadLine();
        Console.Write("Enter Product Name: ");
        string pName = Console.ReadLine();
        Console.Write("Enter Quantity in Stock: ");
        int qtyInStock = int.Parse(Console.ReadLine());
        Console.Write("Enter Price: ");
        double price = double.Parse(Console.ReadLine());
        Console.Write("Enter Discount Allowed (in percentage): ");
        double discountAllowed = double.Parse(Console.ReadLine()) / 100;

        Product newProduct = new Product(pCode, pName, qtyInStock, price, discountAllowed);
        products.Add(newProduct);

        Console.WriteLine("Product added successfully.");
    }

    static void DisplayProducts()
    {
        foreach (var product in products)
        {
            product.DisplayProductDetails();
        }
    }

    static double CalculateTotalAmount(double price, int qty, double discount)
    {
        double totalAmount = price * qty;
        totalAmount -= totalAmount * discount;
        totalAmount -= totalAmount * 0.50; // Additional 50% discount for 26th Jan
        return totalAmount;
    }
}