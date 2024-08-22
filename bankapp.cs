using System;

// Custom Exception for Insufficient Funds
public class InsufficientFundsException : Exception
{
    public InsufficientFundsException(string message) : base(message) { }
}

// Custom Exception for Invalid Amount
public class InvalidAmountException : Exception
{
    public InvalidAmountException(string message) : base(message) { }
}

public class BankAccount
{
    public decimal Balance { get; private set; }

    public BankAccount(decimal initialBalance)
    {
        if (initialBalance < 0)
            throw new InvalidAmountException("Initial balance cannot be negative.");
        Balance = initialBalance;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new InvalidAmountException("Withdrawal amount must be greater than zero.");

        if (amount > Balance)
            throw new InsufficientFundsException("Insufficient funds for this withdrawal.");

        Balance -= amount;
        Console.WriteLine($"Withdrawal successful. New balance: {Balance:C}");
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"Current balance: {Balance:C}");
    }
}

class Program
{
    static void Main()
    {
        BankAccount account = null;

        try
        {
            // Set initial balance to a valid amount for demonstration
            decimal initialBalance = 1000.00m;

            // Create BankAccount object
            account = new BankAccount(initialBalance);
        }
        catch (InvalidAmountException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return; // Exit the application if initialization fails
        }

        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Bank Account Menu");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Withdraw Funds");
            Console.WriteLine("3. Exit");
            Console.Write("Please choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Check Balance
                    account?.DisplayBalance();
                    break;

                case "2":
                    // Withdraw Funds
                    Console.Write("Enter the amount to withdraw: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        try
                        {
                            account?.Withdraw(amount);
                        }
                        catch (InvalidAmountException ex) when (ex.Message.Contains("zero"))
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        catch (InsufficientFundsException ex) when (ex.Message.Contains("Insufficient"))
                        {
                            Console.WriteLine("Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An unexpected error occurred.");
                            Console.WriteLine($"Details: {ex.Message}");
                        }
                        finally
                        {
                            Console.WriteLine("Transaction processing completed.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount entered.");
                    }
                    break;

                case "3":
                    // Exit
                    running = false;
                    Console.WriteLine("Exiting the application. Have a great day!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }

            Console.WriteLine("Press Enter to return to the menu...");
            Console.ReadLine();
        }
    }
}
