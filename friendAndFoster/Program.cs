using System;
using System.Collections.Generic;

enum DogColor
{
    Brown,
    Cream,
    Lilac,
    Spotted,
    Brindle
}

class User
{
    public string Username { get; }
    public string Password { get; }
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

static class UserData
{
    private static readonly List<User> users = new List<User>();
    public static IReadOnlyList<User> Users => users.AsReadOnly();
    public static User LoggedInUser { get; private set; }

    public static bool Register(string username, string password)
    {
        if (users.Exists(u => u.Username == username))
            return false;
        users.Add(new User(username, password));
        return true;
    }

    public static bool Login(string username, string password)
    {
        var user = users.Find(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            LoggedInUser = user;
            return true;
        }
        return false;
    }
}

class Dog
{
    public string Name { get; }
    public string Breed { get; }
    public string State { get; }
    public string City { get; }

    public Dog(string name, string breed, string state, string city)
    {
        Name = name;
        Breed = breed;
        State = state;
        City = city;
    }
}

class State
{
    public string Name { get; }
    public List<string> MajorCities { get; }

    public State(string name, List<string> cities)
    {
        Name = name;
        MajorCities = cities;
    }
}

class Product
{
    public string Name { get; }
    public decimal? HourlyRentalPrice { get; }
    public decimal? DailyRentalPrice { get; }
    public decimal? RentalPrice { get; }
    public decimal PurchasePrice { get; }

    public Product(string name, decimal? hourlyRentalPrice, decimal? dailyRentalPrice, decimal? rentalPrice, decimal purchasePrice)
    {
        Name = name;
        HourlyRentalPrice = hourlyRentalPrice;
        DailyRentalPrice = dailyRentalPrice;
        RentalPrice = rentalPrice;
        PurchasePrice = purchasePrice;
    }
}

class Shop
{
    private readonly List<Product> products = new List<Product>
    {
        new Product("Leash", null, null, 1m, 5m),
        new Product("Harness", null, null, 1m, 10m),
        new Product("Dog Stroller", 5m, 40m, null, 80m),
        new Product("Poop Bags", null, null, 0m, 5m),
        new Product("Dog Ear Wipes", null, null, 0m, 5m),
        new Product("Dog Paw Wipes", null, null, 0m, 5m),
        new Product("Dog Eye Drops", null, null, 0m, 5m),
        new Product("Dog Teeth Cleaning Kit", null, null, 0m, 12m),
        new Product("Deodorizing Wipes", null, null, 0m, 5m),
        new Product("Dog Short Sleeve T-Shirt", null, null, 0m, 5m),
        new Product("Dog Hoodie", null, null, 0m, 10m)
    };

    public void DisplayProducts()
    {
        Console.WriteLine("\n--- Shop ---");
        Console.WriteLine("Available products:");
        for (int i = 0; i < products.Count; i++)
        {
            var p = products[i];
            Console.WriteLine($"{i + 1}. {p.Name} - " +
                $"Rent: ${(p.RentalPrice.HasValue ? p.RentalPrice.Value.ToString("0.##") : "N/A")}, " +
                $"Hourly Rent: ${(p.HourlyRentalPrice.HasValue ? p.HourlyRentalPrice.Value.ToString("0.##") : "N/A")}, " +
                $"Daily Rent: ${(p.DailyRentalPrice.HasValue ? p.DailyRentalPrice.Value.ToString("0.##") : "N/A")}, " +
                $"Buy: ${p.PurchasePrice:0.##}");
        }
    }

    public void HandleShopping()
    {
        DisplayProducts();
        int productIndex = ReadInt("Enter the number of the product you want to interact with: ", 1, products.Count);
        if (productIndex == -1)
        {
            Console.WriteLine("Invalid product selection. Returning to main menu.");
            return;
        }

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
                if (selectedProduct.HourlyRentalPrice.HasValue && selectedProduct.HourlyRentalPrice.Value > 0)
                    Console.WriteLine($"You rented a {selectedProduct.Name} for ${selectedProduct.HourlyRentalPrice.Value:0.##} per hour.");
                else
                    Console.WriteLine("This product is not available for hourly rental.");
                break;
            case "2":
                if (selectedProduct.DailyRentalPrice.HasValue && selectedProduct.DailyRentalPrice.Value > 0)
                    Console.WriteLine($"You rented a {selectedProduct.Name} for ${selectedProduct.DailyRentalPrice.Value:0.##} per day.");
                else
                    Console.WriteLine("This product is not available for daily rental.");
                break;
            case "3":
                Console.WriteLine($"You purchased a {selectedProduct.Name} for ${selectedProduct.PurchasePrice:0.##}.");
                break;
            default:
                Console.WriteLine("Invalid option. Returning to main menu.");
                break;
        }
    }

    private int ReadInt(string prompt, int min, int max)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            return value;
        return -1;
    }
}

static class Data
{
    public static readonly List<State> States = new List<State>
    {
        new State("Alabama", new List<string> { "Birmingham", "Montgomery", "Mobile" }),
        new State("Alaska", new List<string> { "Anchorage", "Fairbanks", "Juneau" }),
        new State("Arizona", new List<string> { "Phoenix", "Tucson", "Mesa" }),
        new State("Arkansas", new List<string> { "Little Rock", "Fort Smith", "Fayetteville" }),
        new State("California", new List<string> { "Los Angeles", "San Francisco", "San Diego" }),
        new State("Colorado", new List<string> { "Denver", "Colorado Springs", "Aurora" }),
        new State("Connecticut", new List<string> { "Bridgeport", "New Haven", "Stamford" }),
        new State("Delaware", new List<string> { "Wilmington", "Dover", "Newark" }),
        new State("Florida", new List<string> { "Jacksonville", "Miami", "Tampa" }),
        new State("Georgia", new List<string> { "Atlanta", "Augusta", "Columbus" }),
        new State("Hawaii", new List<string> { "Honolulu", "Hilo", "Kailua" }),
        new State("Idaho", new List<string> { "Boise", "Meridian", "Nampa" }),
        new State("Illinois", new List<string> { "Chicago", "Aurora", "Naperville" }),
        new State("Indiana", new List<string> { "Indianapolis", "Fort Wayne", "Evansville" }),
        new State("Iowa", new List<string> { "Des Moines", "Cedar Rapids", "Davenport" }),
        new State("Kansas", new List<string> { "Wichita", "Overland Park", "Kansas City" }),
        new State("Kentucky", new List<string> { "Louisville", "Lexington", "Bowling Green" }),
        new State("Louisiana", new List<string> { "New Orleans", "Baton Rouge", "Shreveport" }),
        new State("Maine", new List<string> { "Portland", "Lewiston", "Bangor" }),
        new State("Maryland", new List<string> { "Baltimore", "Frederick", "Rockville" }),
        new State("Massachusetts", new List<string> { "Boston", "Worcester", "Springfield" }),
        new State("Michigan", new List<string> { "Detroit", "Grand Rapids", "Warren" }),
        new State("Minnesota", new List<string> { "Minneapolis", "Saint Paul", "Rochester" }),
        new State("Mississippi", new List<string> { "Jackson", "Gulfport", "Southaven" }),
        new State("Missouri", new List<string> { "Kansas City", "St. Louis", "Springfield" }),
        new State("Montana", new List<string> { "Billings", "Missoula", "Great Falls" }),
        new State("Nebraska", new List<string> { "Omaha", "Lincoln", "Bellevue" }),
        new State("Nevada", new List<string> { "Las Vegas", "Henderson", "Reno" }),
        new State("New Hampshire", new List<string> { "Manchester", "Nashua", "Concord" }),
        new State("New Jersey", new List<string> { "Newark", "Jersey City", "Paterson" }),
        new State("New Mexico", new List<string> { "Albuquerque", "Las Cruces", "Rio Rancho" }),
        new State("New York", new List<string> { "New York City", "Buffalo", "Rochester" }),
        new State("North Carolina", new List<string> { "Charlotte", "Raleigh", "Greensboro" }),
        new State("North Dakota", new List<string> { "Fargo", "Bismarck", "Grand Forks" }),
        new State("Ohio", new List<string> { "Columbus", "Cleveland", "Cincinnati" }),
        new State("Oklahoma", new List<string> { "Oklahoma City", "Tulsa", "Norman" }),
        new State("Oregon", new List<string> { "Portland", "Eugene", "Salem" }),
        new State("Pennsylvania", new List<string> { "Philadelphia", "Pittsburgh", "Allentown" }),
        new State("Rhode Island", new List<string> { "Providence", "Cranston", "Warwick" }),
        new State("South Carolina", new List<string> { "Charleston", "Columbia", "North Charleston" }),
        new State("South Dakota", new List<string> { "Sioux Falls", "Rapid City", "Aberdeen" }),
        new State("Tennessee", new List<string> { "Memphis", "Nashville", "Knoxville" }),
        new State("Texas", new List<string> { "Houston", "Dallas", "Austin" }),
        new State("Utah", new List<string> { "Salt Lake City", "West Valley City", "Provo" }),
        new State("Vermont", new List<string> { "Burlington", "South Burlington", "Rutland" }),
        new State("Virginia", new List<string> { "Virginia Beach", "Norfolk", "Chesapeake" }),
        new State("Washington", new List<string> { "Seattle", "Spokane", "Tacoma" }),
        new State("West Virginia", new List<string> { "Charleston", "Huntington", "Morgantown" }),
        new State("Wisconsin", new List<string> { "Milwaukee", "Madison", "Green Bay" }),
        new State("Wyoming", new List<string> { "Cheyenne", "Casper", "Laramie" }),
    };

    public static readonly List<Dog> Dogs = new List<Dog>
    {
        new Dog("Buddy", "Labrador", "California", "Los Angeles"),
        new Dog("Max", "Poodle", "California", "San Diego"),
        new Dog("Bella", "Bulldog", "Texas", "Houston"),
        new Dog("Lucy", "Beagle", "New York", "New York City"),
    };
}


static class DogSearch
{
    public static void SearchDogsByLocation()
    {
        Console.WriteLine("\n--- Search Dogs by State and City ---");
        for (int i = 0; i < Data.States.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Data.States[i].Name}");
        }
        int stateIndex = ReadInt("Select a state by number: ", 1, Data.States.Count);
        if (stateIndex == -1)
        {
            Console.WriteLine("Invalid state selection.");
            return;
        }

        var selectedState = Data.States[stateIndex - 1];
        Console.WriteLine($"\nMajor cities in {selectedState.Name}:");
        for (int j = 0; j < selectedState.MajorCities.Count; j++)
        {
            Console.WriteLine($"{j + 1}. {selectedState.MajorCities[j]}");
        }
        int cityIndex = ReadInt("Select a city by number: ", 1, selectedState.MajorCities.Count);
        if (cityIndex == -1)
        {
            Console.WriteLine("Invalid city selection.");
            return;
        }

        var selectedCity = selectedState.MajorCities[cityIndex - 1];
        Console.WriteLine($"\nDogs in {selectedCity}, {selectedState.Name}:");
        var foundDogs = Data.Dogs.FindAll(d => d.State == selectedState.Name && d.City == selectedCity);
        if (foundDogs.Count == 0)
        {
            Console.WriteLine("No dogs found in this location.");
        }
        else
        {
            foreach (var dog in foundDogs)
            {
                Console.WriteLine($"- {dog.Name} ({dog.Breed})");
            }
        }
    }

    private static int ReadInt(string prompt, int min, int max)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            return value;
        return -1;
    }
}

static class InputHelper
{
    public static string ReadNonEmptyLine(string prompt)
    {
        string input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input));
        return input;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Shop shop = new Shop();

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Shop");
            Console.WriteLine("4. Search Dogs by State/City");
            Console.WriteLine("5. Exit");

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
                    DogSearch.SearchDogsByLocation();
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void Register()
    {
        string username = InputHelper.ReadNonEmptyLine("Enter a username: ");
        string password = InputHelper.ReadNonEmptyLine("Enter a password: ");
        if (UserData.Register(username, password))
            Console.WriteLine("Registration successful!");
        else
            Console.WriteLine("Username already exists. Please choose another.");
    }

    static void Login()
    {
        string username = InputHelper.ReadNonEmptyLine("Enter your username: ");
        string password = InputHelper.ReadNonEmptyLine("Enter your password: ");
        if (UserData.Login(username, password))
            Console.WriteLine($"Login successful! Welcome, {UserData.LoggedInUser.Username}.");
        else
            Console.WriteLine("Invalid username or password.");
    }
}