using BankApplication.Definitions;

namespace BankApplication.Domain;

public class Deposit : Transaction
{
    private readonly Account _account;
    private readonly decimal _amount;

    public Deposit(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }

    public override void Execute()
    {
        _account.Deposit(_amount);
    }
}
