namespace guess_the_number
{
    using System;

    class NumberGuessingGame
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Number Guessing Game!");

            int min = 1;
            int max = 100;

            Random random = new Random();
            int targetNumber = random.Next(min, max + 1);

            int maxAttempts = 10;
            int attemptsLeft = maxAttempts;

            while (attemptsLeft > 0)
            {
                Console.WriteLine($"You have {attemptsLeft} attempts left.");
                Console.Write($"Guess a number between {min} and {max}: ");

                // Get user's guess and check if it's a valid number
                int userGuess;
                while (!int.TryParse(Console.ReadLine(), out userGuess) || userGuess < min || userGuess > max)
                {
                    Console.Write($"Invalid input. Please enter a number between {min} and {max}: ");
                }

                // Check if the guess is correct
                if (userGuess == targetNumber)
                {
                    Console.WriteLine($"Congratulations! You guessed the correct number: {targetNumber}");
                    break; 
                }
                else
                {
                    if (userGuess < targetNumber)
                    {
                        Console.WriteLine("Your guess is too low. Try again!");
                    }
                    else
                    {
                        Console.WriteLine("Your guess is too high. Try again!");
                    }
                }
                attemptsLeft--;

                if (attemptsLeft == 0)
                {
                    Console.WriteLine($"Game over! The correct number was {targetNumber}. Better luck next time!");
                }
            }

            // Ask if the user wants to play again
            Console.WriteLine("Do you want to play again? (y/n): ");
            string playAgain = Console.ReadLine().ToLower();
            if (playAgain == "y")
            {
                Main(); 
            }
            else
            {
                Console.WriteLine("Thank you for playing! Goodbye!");
            }
        }
    }

}
