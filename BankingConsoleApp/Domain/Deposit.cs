using BankApplication.Definitions;

namespace BankApplication.Domain;

public class Deposit : Transaction
{
    private readonly Account _account;

    public Deposit(Account account, decimal amount) : base(amount)
    {
        _account = account;
    }
}
