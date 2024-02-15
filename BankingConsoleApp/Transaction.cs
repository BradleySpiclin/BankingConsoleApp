namespace BankingConsoleApp;

public abstract class Transaction
{
    protected readonly Guid _transactionId;
    protected decimal _amount;
    protected bool _success;

    private bool _executed;
    private bool _reversed;
    private DateTime _transactionDate;

    public const string TransactionReversed = "Transaction already reversed";
    public const string TransactionFailed = "Unable to rollback failed transaction";
    public const string TransactionExecuted = "Transaction already executed";
    public const string InsufficientFunds = "Insufficient funds to perform transaction";

    public abstract bool Success { get; }

    public bool Executed => _executed;

    public bool Reversed => _reversed;

    public Guid TransactionId => _transactionId;

    public DateTime DateStamp => _transactionDate;

    public Transaction(decimal amount)
    {
        _transactionId = Guid.NewGuid();

        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be greater than 0");

        _amount = amount;
    }

    public abstract void Print();

    public virtual void Execute()
    {
        _executed = true;
        _transactionDate = DateTime.Now;
    }

    public virtual void Rollback()
    {
        _reversed = true;
        _transactionDate = DateTime.Now;
    }
}