using System;

// ourAnimals array will store the following: 
string animalSpecies = "";
string animalID = "";
string animalAge = "";
string animalPhysicalDescription = "";
string animalPersonalityDescription = "";
string animalNickname = "";
string suggestedDonation = "";

// variables that support data entry
int maxPets = 8;
string? readResult;
string menuSelection = "";
decimal decimalDonation = 0.00m;

// array used to store runtime data
string[,] ourAnimals = new string[maxPets, 7];

// sample data ourAnimals array entries
for (int i = 0; i < maxPets; i++)
{
    switch (i)
    {
        case 0:
            animalSpecies = "dog";
            animalID = "d1";
            animalAge = "2";
            animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 45 pounds. housebroken.";
            animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
            animalNickname = "lola";
            suggestedDonation = "85.00";
            break;

        case 1:
            animalSpecies = "dog";
            animalID = "d2";
            animalAge = "9";
            animalPhysicalDescription = "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
            animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
            animalNickname = "gus";
            suggestedDonation = "49.99";
            break;

        case 2:
            animalSpecies = "cat";
            animalID = "c3";
            animalAge = "1";
            animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
            animalPersonalityDescription = "friendly";
            animalNickname = "snow";
            suggestedDonation = "40.00";
            break;

        case 3:
            animalSpecies = "cat";
            animalID = "c4";
            animalAge = "";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "lion";
            suggestedDonation = "";

            break;

        default:
            animalSpecies = "";
            animalID = "";
            animalAge = "";
            animalPhysicalDescription = "";
            animalPersonalityDescription = "";
            animalNickname = "";
            suggestedDonation = "";
            break;

    }

    ourAnimals[i, 0] = "ID #: " + animalID;
    ourAnimals[i, 1] = "Species: " + animalSpecies;
    ourAnimals[i, 2] = "Age: " + animalAge;
    ourAnimals[i, 3] = "Nickname: " + animalNickname;
    ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
    ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;

    if (!decimal.TryParse(suggestedDonation, out decimalDonation))
    {
        decimalDonation = 45.00m; // if suggestedDonation NOT a number, default to 45.00
    }
    ourAnimals[i, 6] = $"Suggested Donation: {decimalDonation:C2}";
}

// top-level menu options
do
{
    // NOTE: the Console.Clear method is throwing an exception in debug sessions
    Console.Clear();

    Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
    Console.WriteLine(" 1. List all of our current pet information");
    Console.WriteLine(" 2. Display all dogs with a specified characteristic");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

    readResult = Console.ReadLine();
    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }

    // switch-case to process the selected menu option
    switch (menuSelection)
    {
        case "1":
            // list all pet info
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 0] != "ID #: ")
                {
                    Console.WriteLine();
                    for (int j = 0; j < 7; j++)
                    {
                        Console.WriteLine(ourAnimals[i, j].ToString());
                    }
                }
            }

            Console.WriteLine("\r\nPress the Enter key to continue");
            readResult = Console.ReadLine();

            break;

        case "2":
            // Display all dogs with multiple search characteristics

            string dogCharacteristicsInput = "";

            while (dogCharacteristicsInput == "")
            {
                // Prompt user to enter multiple comma-separated characteristics to search for
                Console.WriteLine("\r\nEnter dog characteristics to search for separated by commas:");
                readResult = Console.ReadLine();
                if (readResult != null)
                {
                    dogCharacteristicsInput = readResult.ToLower().Trim();
                }
            }

            // Split the input into individual search terms and store them in an array
            string[] dogCharacteristics = dogCharacteristicsInput.Split(',');

            // Sort the search terms in alphanumeric order
            Array.Sort(dogCharacteristics);

            bool foundMatch = false;
            string[] spinningIcons = { "/", "-", "\\" };
            // Loop through each dog in the ourAnimals array
            for (int i = 0; i < maxPets; i++)
            {
                if (ourAnimals[i, 1].Contains("dog"))
                {
                    // Combine physical and personality descriptions
                    string dogDescription = ourAnimals[i, 4] + "\r\n" + ourAnimals[i, 5];

                    // Check if any of the search terms match the dog description
                    foreach (string term in dogCharacteristics)
                    {

                        for (int count = 3; count > 0; count--)
                        {
                            foreach (string icon in spinningIcons)
                            {
                                Console.Write($"\rsearching our dog {ourAnimals[i, 3]} for {term} {icon} {count}");
                                Thread.Sleep(250); // Adjust the speed of animation if needed
                            }
                        }

                        if (dogDescription.Contains(term.Trim()))
                        {
                            // If a match is found, output a message with the dog's name and matched term
                            Console.WriteLine($"\nOur dog {ourAnimals[i, 3]} is a match for your search for {term.Trim()}!");
                            foundMatch = true;
                        }

                        else
                        {
                            // If no match is found, erase the searching animation and display a message
                            Console.Write("\r" + new string(' ', Console.WindowWidth - 1)); // Clear the line
                            Console.Write("\r"); // Move cursor to the beginning of the line
                            Console.WriteLine($"\nNo matches found for {term.Trim()}...");
                        }
                    }
                }
            }

            // If no matches were found for any search term, display a message
            if (!foundMatch)
            {
                Console.WriteLine($"None of our dogs are a match for: {dogCharacteristicsInput}");
            }

            Console.WriteLine("\n\rPress the Enter key to continue");
            readResult = Console.ReadLine();

            break;

        default:
            break;
    }

} while (menuSelection != "exit");
