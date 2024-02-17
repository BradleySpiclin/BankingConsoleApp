namespace BankApplication.Domain;

public class Account
{
    private readonly string _name;
    private readonly long _accountNumber;

    private decimal _balance;

    public string Name => _name;

    public long AccountNumber => _accountNumber;

    public decimal Balance
    {
        get { return _balance; }
        private set { _balance = value; }
    }

    public Account(string name, decimal openingBalance)
    {
        _balance = openingBalance;
        _name = name;
        _accountNumber = GenerateAccountNumber();
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
        Console.WriteLine($"Account: {Name}\nBalance: {Balance:c2}\n");
    }

    private static long GenerateAccountNumber()
    {
        Random random = new();
        return random.Next(100000000, 1000000000);
    }
}

