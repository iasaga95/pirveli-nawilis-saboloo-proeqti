namespace atm
{
    using System;
    using System.IO;

    class ATM
    {
        static string filePath = "accounts.txt";

        static void Main()
        {
            string username = string.Empty;
            double balance = 0;

            Console.WriteLine("Welcome to the ATM System!");

            while (true)
            {
                Console.Write("Enter your username: ");
                username = Console.ReadLine();

                // Load user account from file
                if (LoadUserAccount(username, out balance))
                {
                    Console.WriteLine("Login successful.");
                    break;
                }
                else
                {
                    Console.WriteLine("Account not found. Would you like to create a new account? (y/n): ");
                    string createNew = Console.ReadLine();
                    if (createNew.ToLower() == "y")
                    {
                        Console.Write("Enter your initial deposit: ");
                        while (!double.TryParse(Console.ReadLine(), out balance) || balance <= 0)
                        {
                            Console.Write("Invalid input. Please enter a valid amount to deposit: ");
                        }
                        CreateAccount(username, balance);
                        Console.WriteLine("Account created successfully!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Goodbye!");
                        return;
                    }
                }
            }

            // Main ATM operations
            while (true)
            {
                Console.Clear();
                Console.WriteLine("ATM Operations:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Logout");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine($"Your current balance is: ${balance}");
                        break;

                    case "2":
                        Console.Write("Enter the amount to deposit: ");
                        double depositAmount;
                        while (!double.TryParse(Console.ReadLine(), out depositAmount) || depositAmount <= 0)
                        {
                            Console.Write("Invalid input. Please enter a valid deposit amount: ");
                        }
                        balance += depositAmount;
                        UpdateAccount(username, balance);
                        Console.WriteLine($"Successfully deposited ${depositAmount}. New balance is: ${balance}");
                        break;

                    case "3":
                        Console.Write("Enter the amount to withdraw: ");
                        double withdrawAmount;
                        while (!double.TryParse(Console.ReadLine(), out withdrawAmount) || withdrawAmount <= 0 || withdrawAmount > balance)
                        {
                            if (withdrawAmount > balance)
                                Console.Write("Insufficient funds. ");
                            else
                                Console.Write("Invalid amount. ");
                            Console.Write("Please enter a valid withdrawal amount: ");
                        }
                        balance -= withdrawAmount;
                        UpdateAccount(username, balance);
                        Console.WriteLine($"Successfully withdrew ${withdrawAmount}. New balance is: ${balance}");
                        break;

                    case "4":
                        TransferMoney(username, ref balance);
                        break;

                    case "5":
                        Console.WriteLine("Logging out...");
                        return;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static bool LoadUserAccount(string username, out double balance)
        {
            balance = 0;
            if (!File.Exists(filePath))
                return false;

            string[] accounts = File.ReadAllLines(filePath);

            foreach (string account in accounts)
            {
                string[] parts = account.Split(',');
                if (parts[0] == username)
                {
                    balance = double.Parse(parts[1]);
                    return true;
                }
            }
            return false;
        }

        // Create a new user account and save it to the file
        static void CreateAccount(string username, double balance)
        {
            string newAccount = $"{username},{balance}";
            File.AppendAllText(filePath, newAccount + Environment.NewLine);
        }

        // Update the user's account balance in the file
        static void UpdateAccount(string username, double balance)
        {
            if (!File.Exists(filePath))
                return;

            string[] accounts = File.ReadAllLines(filePath);
            for (int i = 0; i < accounts.Length; i++)
            {
                string[] parts = accounts[i].Split(',');
                if (parts[0] == username)
                {
                    accounts[i] = $"{username},{balance}";
                    break;
                }
            }
            File.WriteAllLines(filePath, accounts);
        }

        // Function for transferring money to another user
        static void TransferMoney(string senderUsername, ref double senderBalance)
        {
            Console.Write("Enter the recipient's username: ");
            string recipientUsername = Console.ReadLine();

            if (!LoadUserAccount(recipientUsername, out double recipientBalance))
            {
                Console.WriteLine("Recipient account not found.");
                return;
            }

            Console.Write("Enter the amount to transfer: ");
            double transferAmount;
            while (!double.TryParse(Console.ReadLine(), out transferAmount) || transferAmount <= 0 || transferAmount > senderBalance)
            {
                if (transferAmount > senderBalance)
                    Console.Write("Insufficient funds. ");
                else
                    Console.Write("Invalid amount. ");
                Console.Write("Please enter a valid transfer amount: ");
            }

            senderBalance -= transferAmount;
            recipientBalance += transferAmount;

            UpdateAccount(senderUsername, senderBalance);
            UpdateAccount(recipientUsername, recipientBalance);

            Console.WriteLine($"Successfully transferred ${transferAmount} to {recipientUsername}.");
            Console.WriteLine($"Your new balance is: ${senderBalance}");
        }
    }

}
