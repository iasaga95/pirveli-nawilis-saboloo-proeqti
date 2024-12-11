namespace calculator
{
    using System;


    class Calculator
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Calculator!");

            double num1, num2, result;
            string operation;

            while (true)
            {
                Console.Write("Enter the first number: ");
                while (!double.TryParse(Console.ReadLine(), out num1)) 
                {
                    Console.Write("Invalid input. Please enter a valid number for the first number: ");
                }

                Console.Write("Enter the second number: ");
                while (!double.TryParse(Console.ReadLine(), out num2)) 
                {
                    Console.Write("Invalid input. Please enter a valid number for the second number: ");
                }

                // Ask the user to select an operation
                Console.WriteLine("Select an operation:");
                Console.WriteLine(" + for Addition");
                Console.WriteLine(" - for Subtraction");
                Console.WriteLine(" * for Multiplication");
                Console.WriteLine(" / for Division");
                operation = Console.ReadLine();

                if (operation != "+" && operation != "-" && operation != "*" && operation != "/")
                {
                    Console.WriteLine("Invalid operation selected. Please choose one of +, -, *, /.");
                    continue;
                }

                // Perform the calculation
                switch (operation)
                {
                    case "+":
                        result = num1 + num2;
                        Console.WriteLine($"The result of {num1} + {num2} = {result}");
                        break;

                    case "-":
                        result = num1 - num2;
                        Console.WriteLine($"The result of {num1} - {num2} = {result}");
                        break;

                    case "*":
                        result = num1 * num2;
                        Console.WriteLine($"The result of {num1} * {num2} = {result}");
                        break;

                    case "/":
                        // Handle division by zero
                        if (num2 == 0)
                        {
                            Console.WriteLine("Error: Division by zero is not allowed.");
                        }
                        else
                        {
                            result = num1 / num2;
                            Console.WriteLine($"The result of {num1} / {num2} = {result}");
                        }
                        break;

                    default:
                        Console.WriteLine("An unexpected error occurred.");
                        break;
                }

                // Ask if the user wants to perform another calculation
                Console.Write("Do you want to perform another calculation? (y/n): ");
                string continueCalculation = Console.ReadLine().ToLower();
                if (continueCalculation != "y")
                {
                    break; 
                }
            }

            Console.WriteLine("Thank you for using the calculator. Goodbye!");
        }
    }
}

