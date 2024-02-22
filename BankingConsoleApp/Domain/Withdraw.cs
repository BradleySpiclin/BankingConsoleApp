using BankApplication.Definitions;
using BankApplication.Interfaces;

namespace BankApplication.Domain;

public class Withdraw : Transaction
{
    private readonly Account _account;

    public Withdraw(Account account, decimal amount) : base(amount)
    {
        _account = account;

        if (amount > _account.Balance)
            throw new ArgumentException("Cannot withdraw more than balance.");
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }
}