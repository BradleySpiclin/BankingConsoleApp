namespace BankingConsoleApp;

public class WithdrawTransaction : Transaction
{
    private readonly Account _account;

    public override bool Success => _success;

    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Print()
    {
        Console.WriteLine("\n** Withdraw successful **");
        Console.WriteLine($"Account: {_account.Name}\nAmount: {_amount:c2}\n");
    }

    public override void Execute()
    {
        if (Executed)
        {
            throw new InvalidOperationException(TransactionExecuted);
        }

        base.Execute();

        if (_account.Withdraw(_amount))
        {
            _success = true;
        }
        else
        {
            throw new InvalidOperationException(InsufficientFunds);
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
            _account.Deposit(_amount);
            base.Rollback();
        }
        else
        {
            throw new InvalidOperationException(TransactionFailed);
        }
    }
}