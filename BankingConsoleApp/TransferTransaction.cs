namespace BankingConsoleApp;

public class TransferTransaction : Transaction
{
    private readonly Account _fromAccount;
    private readonly Account _toAccount;
    private readonly DepositTransaction _depositTransaction;
    private readonly WithdrawTransaction _withdrawTransaction;

    public override bool Success => _withdrawTransaction.Success && _depositTransaction.Success;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
        _withdrawTransaction = new WithdrawTransaction(fromAccount, amount);
        _depositTransaction = new DepositTransaction(toAccount, amount);
    }

    public override void Print()
    {
        Console.WriteLine("\n** Transfer successful **");
        Console.WriteLine($"From account: {_fromAccount.Name}\nTo account: {_toAccount.Name}");
        Console.WriteLine($"Amount: {_amount:c2}");
    }

    public override void Execute()
    {
        if (Executed)
        {
            throw new InvalidOperationException(TransactionExecuted);
        }

        base.Execute();

        try
        {
            _withdrawTransaction.Execute();
            _depositTransaction.Execute();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public override void Rollback()
    {
        if (Reversed)
        {
            throw new InvalidOperationException(TransactionReversed);
        }

        if (Success)
        {
            _depositTransaction.Rollback();
            _withdrawTransaction.Rollback();
            base.Rollback();
        }
        else
        {
            throw new InvalidOperationException(TransactionFailed);
        }
    }
}