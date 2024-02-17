using System.Runtime.InteropServices.Marshalling;

namespace BankApplication.Definitions;

public abstract class Transaction
{
    public Guid Id { get; }
    public decimal Amount { get; }
    public DateTime Date { get; }

    protected Transaction(decimal amount)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Date = DateTime.Now;
    }

    public virtual void Execute() { }

    public virtual void Print() { }

}