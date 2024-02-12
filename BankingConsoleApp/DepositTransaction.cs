namespace BankingConsoleApp;

public class DepositTransaction : Transaction
{
    private readonly Account _account;

    public override bool Success => _success;

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Print()
    {
        Console.WriteLine("\n** Deposit successful **");
        Console.WriteLine($"Account: {_account.Name}\nAmount: {_amount:c2}\n");
    }

    public override void Execute()
    {
        if (Executed)
        {
            throw new InvalidOperationException(TransactionExecuted);
        }

        base.Execute();

        if (_account.Deposit(_amount))
        {
            _success = true;
        }
        else
        {
            throw new InvalidOperationException("Unable to deposit funds.");
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
            if (_account.Withdraw(_amount))
            {
                base.Rollback();
            }
            else
            {
                throw new InvalidOperationException(InsufficientFunds);
            }
        }
        else
        {
            throw new InvalidOperationException(TransactionFailed);
        }
    }
}
