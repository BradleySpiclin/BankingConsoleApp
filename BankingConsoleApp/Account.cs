namespace BankingConsoleApp;

public class Account
{
    private decimal _balance;
    private string _name;

    public string Name
    {
        get { return _name; }
        private set { _name = value; }
    }

    public decimal Balance
    {
        get { return _balance; }
        private set { _balance = value; }
    }

    public Account(string name, decimal balance)
    {
        Balance = balance;
        Name = name;
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
}

