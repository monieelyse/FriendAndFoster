using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Enum for Dog Colors
enum DogColor
{
    Brown,
    Cream,
    Lilac,
    Spotted,
    Brindle
}

// Enum for Dog

// Updated Product class to support hourly and daily rental rates
class Product
{
    public string Name { get; set; }
    public decimal RentalPrice { get; set; } // For simple rental
    public decimal DailyRentalPrice { get; set; } // For daily rental
    public decimal HourlyRentalPrice { get; set; } // For hourly rental
    public decimal PurchasePrice { get; set; }

    public Product(string name, decimal rentalPrice, decimal purchasePrice)
    {
        Name = name;
        RentalPrice = rentalPrice;
        PurchasePrice = purchasePrice;
    }

    public Product(string name, decimal hourlyRentalPrice, decimal dailyRentalPrice, decimal purchasePrice)
    {
        Name = name;
        HourlyRentalPrice = hourlyRentalPrice;
        DailyRentalPrice = dailyRentalPrice;
        PurchasePrice = purchasePrice;
    }
}

// Updated Shop class with new products
class Shop
{
    private List<Product> products = new List<Product>
    {
        new Product("Leash", 1m, 5m),
        new Product("Harness", 1m, 10m),
        new Product("Dog Stroller", 5m, 40m, 80m), // Hourly, Daily, Purchase
        new Product("Poop Bags", 0m, 5m),
        new Product("Dog Ear Wipes", 0m, 5m),
        new Product("Dog Paw Wipes", 0m, 5m),
        new Product("Dog Eye Drops", 0m, 5m),
        new Product("Dog Teeth Cleaning Kit", 0m, 12m),
        new Product("Deodorizing Wipes", 0m, 5m),
        new Product("Dog Short Sleeve T-Shirt", 0m, 5m),
        new Product("Dog Hoodie", 0m, 10m)
    };

    public void DisplayProducts()
    {
        Console.WriteLine("\n--- Shop ---");
        Console.WriteLine("Available products:");
        for (int i = 0; i < products.Count; i++)
        {
            var product = products[i];
            Console.WriteLine($"{i + 1}. {product.Name} - Rent: ${product.RentalPrice} (if applicable), Hourly Rent: ${product.HourlyRentalPrice} (if applicable), Daily Rent: ${product.DailyRentalPrice} (if applicable), Buy: ${product.PurchasePrice}");
        }
    }

    public void HandleShopping()
    {
        DisplayProducts();
        Console.WriteLine("Enter the number of the product you want to interact with:");
        if (int.TryParse(Console.ReadLine(), out int productIndex) && productIndex > 0 && productIndex <= products.Count)
        {
            Product selectedProduct = products[productIndex - 1];
            Console.WriteLine($"You selected: {selectedProduct.Name}");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Rent (Hourly)");
            Console.WriteLine("2. Rent (Daily)");
            Console.WriteLine("3. Buy");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    if (selectedProduct.HourlyRentalPrice > 0)
                        Console.WriteLine($"You rented a {selectedProduct.Name} for ${selectedProduct.HourlyRentalPrice} per hour.");
                    else
                        Console.WriteLine("This product is not available for hourly rental.");
                    break;
                case "2":
                    if (selectedProduct.DailyRentalPrice > 0)
                        Console.WriteLine($"You rented a {selectedProduct.Name} for ${selectedProduct.DailyRentalPrice} per day.");
                    else
                        Console.WriteLine("This product is not available for daily rental.");
                    break;
                case "3":
                    Console.WriteLine($"You purchased a {selectedProduct.Name} for ${selectedProduct.PurchasePrice}.");
                    break;
                default:
                    Console.WriteLine("Invalid option. Returning to main menu.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid product selection. Returning to main menu.");
        }
    }
}

// ...existing code...

static void Main(string[] args)
{
    Shop shop = new Shop();

    while (true)
    {
        Console.WriteLine("\nChoose an option:");
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.WriteLine("3. Shop");
        Console.WriteLine("4. Exit");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Register();
                break;
            case "2":
                Login();
                break;
            case "3":
                shop.HandleShopping();
                break;
            case "4":
                Console.WriteLine("Goodbye!");
                return;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
}
