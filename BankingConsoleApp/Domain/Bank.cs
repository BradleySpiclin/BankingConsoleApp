using BankApplication.Definitions;
using BankApplication.Interfaces;

namespace BankApplication.Domain;

public class Bank : IBank
{
    private readonly List<Account> _accounts = [];

    public static string Name => "Welcome to See Sharp Bank";

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public Account GetAccount(string accountName)
    {
        return _accounts
            .FirstOrDefault(account => account.Name == accountName);
    }

    public void ExecuteTransaction(Account account, Transaction transaction)
    {
        transaction.Execute();

        account.RecordTransaction(transaction);
    }

    public void PrintTransactionHistory(Account account)
    {
        account.DisplayTransactionHistory();
    }

    public bool GetAccount(long accountNumber)
    {
        throw new NotImplementedException();
    }

    public bool GetPin(int pinNumber)
    {
        throw new NotImplementedException();
    }

    public Account AccessAccount(bool isAuthorised)
    {
        throw new NotImplementedException();
    }

    public string PrintStatus(bool status)
    {
        throw new NotImplementedException();
    }
}