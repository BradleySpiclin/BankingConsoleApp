using BankApplication.Definitions;

namespace BankApplication.Domain;

public class Account
{
    private readonly List<Transaction> _transactions = [];
    private readonly string _firstName;
    private readonly string _lastName;
    private readonly int _pin;

    private decimal _balance;

    public string FirstName => _firstName;
    public string LastName => _lastName;
    public int Pin => _pin;

    public decimal Balance
    {
        get { return _balance; }
        private set { _balance = value; }
    }

    public Account(string firstName, string lastName, decimal openingBalance, int pin)
    {
        _balance = openingBalance;
        _firstName = firstName;
        _lastName = lastName;
        _pin = pin;
    }

    public bool Deposit(decimal amount)
    {
        if (amount >= 0)
        {
            Balance += amount;
            return true;
        }

        return false;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= Balance && amount >= 0)
        {
            Balance -= amount;
            return true;
        }

        return false;
    }

    public void Print()
    {
        
    }

    public void RecordTransaction(Transaction transaction) 
    {
        _transactions.Add(transaction);
    }

    public void DisplayTransactionHistory() 
    {
        foreach (var transaction in _transactions)
        {
            //transaction.Print();
        }
    }
}

