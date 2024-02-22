namespace BankApplication.Definitions;

public abstract class Transaction
{
    private readonly Guid _Id;
    private readonly decimal _amount;
    private readonly DateTime _created;
    private DateTime _modified;

    protected Transaction(decimal amount)
    {
        _Id = Guid.NewGuid();
        _amount = amount;
        _created = DateTime.Now;
    }

    public abstract void Execute();

    private void Print() 
    {
        Console.WriteLine($"{GetType().Name} : Created - {_created} : Modified {_modified}");
    }
}