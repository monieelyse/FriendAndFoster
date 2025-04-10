using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    // In-memory user store
    static Dictionary<string, string> users = new Dictionary<string, string>();

    // In-memory store for valid admin-provided verification codes
    static HashSet<string> validVerificationCodes = new HashSet<string> { "SAFE", "LOVE", "HAPPY", "FRIEND" };

    // In-memory store for dog profiles
    static List<string> dogProfiles = new List<string>
    {
        "Buddy - Golden Retriever, 2 years old, loves playing fetch.",
        "Bella - Labrador, 3 years old, very friendly and loves kids.",
        "Charlie - Beagle, 1 year old, energetic and curious.",
        "Lucy - Poodle, 4 years old, calm and loves cuddles."
    };

    // In-memory store for categorized dog profiles
    static Dictionary<string, Dictionary<string, List<string>>> categorizedDogProfiles = new Dictionary<string, Dictionary<string, List<string>>>
    {
        {
            "Toy Breed", new Dictionary<string, List<string>>
            {
                { "0-2", new List<string> { "Milo - Chihuahua, 1 year old, playful and energetic." } },
                { "3-5", new List<string> { "Luna - Pomeranian, 4 years old, loves attention." } },
                { "5-8", new List<string> { "Coco - Yorkshire Terrier, 6 years old, calm and affectionate." } },
                { "8-20", new List<string> { "Bella - Shih Tzu, 10 years old, gentle and loving." } }
            }
        },
        {
            "Small Breed", new Dictionary<string, List<string>>
            {
                { "0-2", new List<string> { "Charlie - Beagle, 1 year old, curious and energetic." } },
                { "3-5", new List<string> { "Max - Dachshund, 3 years old, loyal and playful." } },
                { "5-8", new List<string> { "Ruby - French Bulldog, 7 years old, calm and friendly." } },
                { "8-20", new List<string> { "Oscar - Cocker Spaniel, 9 years old, affectionate and gentle." } }
            }
        },
        {
            "Intermediate Breed", new Dictionary<string, List<string>>
            {
                { "0-2", new List<string> { "Buddy - Border Collie, 2 years old, highly intelligent and active." } },
                { "3-5", new List<string> { "Daisy - Australian Shepherd, 4 years old, friendly and energetic." } },
                { "5-8", new List<string> { "Rocky - Siberian Husky, 6 years old, playful and strong." } },
                { "8-20", new List<string> { "Molly - Boxer, 10 years old, loyal and protective." } }
            }
        },
        {
            "Large Breed", new Dictionary<string, List<string>>
            {
                { "0-2", new List<string> { "Zeus - Great Dane, 2 years old, gentle giant." } },
                { "3-5", new List<string> { "Thor - Rottweiler, 4 years old, confident and calm." } },
                { "5-8", new List<string> { "Bruno - German Shepherd, 7 years old, intelligent and loyal." } },
                { "8-20", new List<string> { "Maximus - Saint Bernard, 12 years old, loving and gentle." } }
            }
        },
        {
            "Size", new Dictionary<string, List<string>>
            {
                { "0-4 pounds", new List<string> { "Tiny - Chihuahua, 1 year old, hypoallergenic, white." } },
                { "6-12 pounds", new List<string> { "Luna - Pomeranian, 4 years old, hypoallergenic, orange." } },
                { "12-20 pounds", new List<string> { "Coco - French Bulldog, 3 years old, non-hypoallergenic, black." } },
                { "20-30 pounds", new List<string> { "Max - Beagle, 5 years old, non-hypoallergenic, dapple." } },
                { "30 pounds and above", new List<string> { "Buddy - Golden Retriever, 2 years old, non-hypoallergenic, red." } }
            }
        },
        {
            "Age", new Dictionary<string, List<string>>
            {
                { "0-3 years", new List<string> { "Charlie - Beagle, 1 year old, non-hypoallergenic, black." } },
                { "4-7 years", new List<string> { "Ruby - Labrador, 5 years old, non-hypoallergenic, grey." } },
                { "8-11 years", new List<string> { "Oscar - Dachshund, 9 years old, hypoallergenic, white." } },
                { "12 and older", new List<string> { "Bella - Shih Tzu, 12 years old, hypoallergenic, orange." } }
            }
        },
        {
            "Hypoallergenic", new Dictionary<string, List<string>>
            {
                { "Yes", new List<string> { "Lucy - Poodle, 4 years old, hypoallergenic, white." } },
                { "No", new List<string> { "Buddy - Golden Retriever, 2 years old, non-hypoallergenic, red." } }
            }
        },
        {
            "Color", new Dictionary<string, List<string>>
            {
                { "White", new List<string> { "Oscar - Dachshund, 9 years old, hypoallergenic, white." } },
                { "Black", new List<string> { "Charlie - Beagle, 1 year old, non-hypoallergenic, black." } },
                { "Dapple", new List<string> { "Max - Beagle, 5 years old, non-hypoallergenic, dapple." } },
                { "Orange", new List<string> { "Bella - Shih Tzu, 12 years old, hypoallergenic, orange." } },
                { "Red", new List<string> { "Buddy - Golden Retriever, 2 years old, non-hypoallergenic, red." } },
                { "Grey", new List<string> { "Ruby - Labrador, 5 years old, non-hypoallergenic, grey." } }
            }
        }
    };

    static void Main(string[] args)
    {
        string prompt = "Welcome to Friend and Foster! We are paw-sitively proud to have you here! In order to start friend and foster-ing, please register for a new account or log in to continue. **Note: You will need an approval code from admin upon completing a verified background check";

        Console.WriteLine(prompt);

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");

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
        Console.WriteLine("\n--- Registration ---");
        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        if (!IsValidEmail(email))
        {
            Console.WriteLine("Invalid email format. Please try again.");
            return;
        }

        if (users.ContainsKey(email))
        {
            Console.WriteLine("Email already exists. Please try a different one.");
            return;
        }

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        Console.Write("Enter verification code provided by admin: ");
        string verificationCode = Console.ReadLine();

        if (!validVerificationCodes.Contains(verificationCode))
        {
            Console.WriteLine("Invalid verification code. Please contact admin for a valid code.");
            return;
        }

        users[email] = password;
        Console.WriteLine("Registration successful! Your account has been verified.");

        // Redirect to dog profiles
        ShowDogProfiles();
    }

    static void ShowDogProfiles()
    {
        Console.WriteLine("Select a category to filter dog profiles:");
        Console.WriteLine("1. Breed");
        Console.WriteLine("2. Size");
        Console.WriteLine("3. Age");
        Console.WriteLine("4. Hypoallergenic");
        Console.WriteLine("5. Color");

        string categoryChoice = Console.ReadLine();
        string selectedCategory = categoryChoice switch
        {
            "1" => "Breed",
            "2" => "Size",
            "3" => "Age",
            "4" => "Hypoallergenic",
            "5" => "Color",
            _ => null
        };

        if (selectedCategory == null || !categorizedDogProfiles.ContainsKey(selectedCategory))
        {
            Console.WriteLine("Invalid category. Returning to main menu.");
            return;
        }

        Console.WriteLine($"Select a subcategory for {selectedCategory}:");
        foreach (var subcategory in categorizedDogProfiles[selectedCategory].Keys)
        {
            Console.WriteLine($"- {subcategory}");
        }

        string subcategoryChoice = Console.ReadLine();
        if (categorizedDogProfiles[selectedCategory].ContainsKey(subcategoryChoice))
        {
            Console.WriteLine($"Dog profiles in {subcategoryChoice}:");
            foreach (var profile in categorizedDogProfiles[selectedCategory][subcategoryChoice])
            {
                Console.WriteLine(profile);
            }
        }
        else
        {
            Console.WriteLine("Invalid subcategory. Returning to main menu.");
        }
    }

    static void Login()
    {
        Console.WriteLine("\n--- Login ---");
        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        if (!users.ContainsKey(email))
        {
            Console.WriteLine("Email not found. Please register first.");
            return;
        }

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        if (users[email] == password)
        {
            Console.WriteLine("Login successful! Welcome, " + email + "!");
        }
        else
        {
            Console.WriteLine("Incorrect password. Please try again.");
        }
    }

    static bool IsValidEmail(string email)
    {
        // Simple email validation using regex
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }
}
