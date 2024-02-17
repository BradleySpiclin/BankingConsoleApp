namespace BankApplication.Interfaces;

public interface ITransaction
{
    public Guid TransactionId();

    public DateTime DateStamp();
    public abstract bool Success();

    public bool Executed();

    public bool Reversed();

    public abstract void Print();

    public abstract void Execute();

    public abstract void Rollback();
}
