using BankApplication.Definitions;

namespace BankApplication.Domain;

public class Transfer : Transaction
{
    private readonly Account _fromAccount;
    private readonly Account _toAccount;

    public Transfer(Account fromAccount,
        Account toAccount, decimal amount) : base(amount)
    {
        _fromAccount = fromAccount;
        _toAccount = toAccount;
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }
}