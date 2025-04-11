![Logo](logo.jpeg)

## Friend and Foster App ##

[❣ Contribute to funding Friend and Foster App ❣](https://www.paypal.com/paypalme/monieelyse)


I am developing a cloud-based application that allows background-verified personnel to rent rescue dogs from local shelters.



#### This code implements a console-based application called "Friend and Foster," which allows users to register, log in, and view categorized dog profiles. Here's a breakdown of how it works: ####
---
### 1. Data Storage ###

•	Users: Stored in an in-memory dictionary (users) where the key is the email, and the value is the password.

•	Verification Codes: A HashSet (validVerificationCodes) contains predefined admin-provided codes for account verification.

•	Dog Profiles:

•	A List<string> (dogProfiles) stores general dog profiles.

•	A nested dictionary (categorizedDogProfiles) organizes dog profiles by breed categories (e.g., "Toy Breed") and additional categories such as size, age, hypoallergenic status, and color.

---
### 2. Main Program Flow ###
The Main method serves as the entry point and displays a welcome message. It provides three options:
1.	Register: Calls the Register method to create a new user account.
2.	Login: Calls the Login method to authenticate an existing user.
3.	Exit: Terminates the program.
---
### 3. Registration Process ###
The Register method:
1.	Prompts the user for an email, password, and verification code.
2.	Validates the email format using the IsValidEmail method (regex-based).
3.	Checks if the email already exists in the users dictionary.
4.	Verifies the provided code against the validVerificationCodes set.
5.	If all checks pass, the user is registered, and their email/password is stored in the users dictionary.
6.	Redirects the user to view dog profiles by calling ShowDogProfiles.
---
### 4. Viewing Dog Profiles ###
The ShowDogProfiles method:
1.	Prompts the user to select a category (e.g., "Breed", "Size", "Age", "Hypoallergenic", "Color").
2.	Prompts the user to select a subcategory within the chosen category:
   - **Size**: Options include "0-4 pounds", "6-12 pounds", "12-20 pounds", "20-30 pounds", "30 pounds and above".
   - **Age**: Options include "0-3 years", "4-7 years", "8-11 years", "12 and older".
   - **Hypoallergenic**: Options include "Yes" and "No".
   - **Color**: Options include "White", "Black", "Dapple", "Orange", "Red", "Grey".
3.	Displays the corresponding dog profiles from the categorizedDogProfiles dictionary.
4.	Handles invalid inputs by returning to the main menu.
---
### 5. Login Process ###
The Login method:
1.	Prompts the user for their email and password.
2.	Checks if the email exists in the users dictionary.
3.	Verifies the password against the stored value.
4.	Displays a success message if the credentials are correct; otherwise, it notifies the user of incorrect credentials.
---
### 6. Utility Method ###

•	IsValidEmail: Uses a regular expression to validate the format of an email address.

---
### Key Features ###
•	User Authentication: Ensures only verified users can register and log in.
•	Categorized Dog Profiles: Allows users to explore dog profiles based on breed, size, age, hypoallergenic status, and color.
•	Error Handling: Provides feedback for invalid inputs (e.g., incorrect email format, invalid category/age selection).

