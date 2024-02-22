using BankApplication.Definitions;
using BankApplication.Domain;

namespace BankApplication.Interfaces;

public interface IBank
{
    public void AddAccount(Account account);

    public bool GetAccount(long accountNumber);

    public bool GetPin(int pinNumber);

    public Account AccessAccount(bool isAuthorised);

    public void ExecuteTransaction(Account account, Transaction transaction);

    public string PrintStatus(bool status);
}
