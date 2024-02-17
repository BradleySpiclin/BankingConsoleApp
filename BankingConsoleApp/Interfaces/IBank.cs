using BankApplication.Definitions;
using BankApplication.Domain;

namespace BankApplication.Interfaces;

public interface IBank
{
    public void AddAccount(Account account);

    public Account GetAccount(string accountName);

    public void ExecuteTransaction(Transaction transaction);

    public void RollbackTransaction(Transaction transaction);

    public void PrintTransactionHistory();

    public Transaction GetTransactionAtIndex(int index);

    public string PrintStatus(bool status);
}
