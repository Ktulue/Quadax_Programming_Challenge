using System;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Console Mastermind in C#");
        Console.WriteLine("------------------------");
        Console.WriteLine("Guess the 4-digit code (numbers 1-6).");
        Console.WriteLine("You only have 10 attempts to guess the correct number.");
        Console.WriteLine("");

        int userAttempts = 10;
        string[] correctAnswer = { "+", "+", "+", "+" };
        string[] randomNumberGenerated = GenerateRandomCombination();

        while (userAttempts > 0)
        {
            Console.WriteLine("Enter your guess: ");
            string[] guess = ReadUserGuess();

            string[] feedback = CheckGuess(randomNumberGenerated, guess);

            if (string.Join("", feedback) == string.Join("", correctAnswer))
            {
                Console.WriteLine("Congrats! You guessed the correct number!");
                break;
            }
            else
            {
                Console.WriteLine(string.Join("", feedback));
            }

            userAttempts--;

            if (userAttempts == 0)
            {
                Console.WriteLine("You ran out of attempts. :(");
                Console.WriteLine("The correct answer was: " + string.Join("", randomNumberGenerated));
                break;
            }

            Console.WriteLine("You have {0} attempts left.", userAttempts);
            Console.WriteLine("");
        }
    }

    static string[] GenerateRandomCombination()
    {
        string[] randomCombo = new string[4];
        Random random = new Random();

        for (int i = 0; i < 4; i++)
        {
            randomCombo[i] = random.Next(1, 7).ToString();
        }

        return randomCombo;
    }

    static string[] ReadUserGuess()
    {
        string userGuess = Console.ReadLine();
        string[] parseNumber = new string[4];

        if (!string.IsNullOrEmpty(userGuess) && userGuess.Length == 4)
        {
            bool isValid = true;
            for (int i = 0; i < 4; i++)
            {
                if (userGuess[i] < '1' || userGuess[i] > '6')
                {
                    isValid = false;
                    break;
                }
                parseNumber[i] = userGuess.Substring(i, 1);
            }

            if (!isValid)
            {
                Console.WriteLine("Please enter a valid 4-digit guess with numbers between 1 and 6. Please try again.");
                return ReadUserGuess(); // Prompt the user to enter a valid guess again
            }
        }
        else
        {
            Console.WriteLine("Please enter a valid 4-digit guess. Not guessing or incorrect length is invalid. Please try again.");
            return ReadUserGuess(); // Prompt the user to enter a valid guess again
        }

        return parseNumber;
    }

    static string[] CheckGuess(string[] randomNumberGenerated, string[] userGuess)
    {
        string[] feedback = new string[4];
        bool[] isDigitUsed = new bool[4]; // To track used digits in the generated combination

        // First pass: Check for correct digits in the correct position
        for (int i = 0; i < 4; i++)
        {
            if (randomNumberGenerated[i] == userGuess[i])
            {
                feedback[i] = "+";
                isDigitUsed[i] = true;
            }
        }

        // Second pass: Check for correct digits in the wrong position
        for (int i = 0; i < 4; i++)
        {
            if (feedback[i] != "+") // Skip already matched digits
            {
                for (int a = 0; a < 4; a++)
                {
                    if (!isDigitUsed[a] && randomNumberGenerated[a] == userGuess[i])
                    {
                        feedback[i] = "-";
                        isDigitUsed[a] = true;
                        break;
                    }
                }
            }
        }

        // Sort feedback to print all plus signs first, then minus signs
        Array.Sort(feedback, (x, y) => string.Compare(y, x));

        return feedback;
    }
}
