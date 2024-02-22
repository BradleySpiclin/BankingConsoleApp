namespace BankApplication.Interfaces;

public interface ITransaction
{
    public Guid TransactionId();

    public DateTime DateStamp();

    public abstract bool Success();

    public bool Executed();

    public abstract void Print();

    public abstract void Execute();
}
