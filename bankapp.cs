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
}

class Program
{
    static void Main()
    {
        var account = new BankAccount(1000.00m); // Initial balance of $1000

        try
        {
            // Attempt to withdraw $1500, which should fail
            account.Withdraw(1500.00m);
        }
        catch (InvalidAmountException ex) when (ex.Message.Contains("zero"))
        {
            // Handle cases where the amount is zero or negative
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (InsufficientFundsException ex) when (ex.Message.Contains("Insufficient"))
        {
            // Handle insufficient funds specifically
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Handle any other unexpected exceptions
            Console.WriteLine("An unexpected error occurred.");
            Console.WriteLine($"Details: {ex.Message}");
        }
        finally
        {
            // Final operations, e.g., logging
            Console.WriteLine("Transaction processing completed.");
        }
    }
}
