namespace BankingConsoleApp;

public class Bank : IBank
{
    private readonly List<Account> _accounts = [];
    private readonly List<Transaction> _transactions = [];

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public Account GetAccount(string accountName)
    {
        return _accounts
            .FirstOrDefault(account => account.Name == accountName);
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
        transaction.Execute();
    }

    public void RollbackTransaction(Transaction transaction)
    {
        transaction.Rollback();
    }

    public void PrintTransactionHistory()
    {
        int itemNumber = 1;

        foreach (var transaction in _transactions)
        {
            Console.WriteLine($"{itemNumber}: {transaction.GetType().Name} | {PrintStatus(transaction.Success)} on: {transaction.DateStamp}");
            itemNumber++;
        }
    }

    public Transaction GetTransactionAtIndex(int index)
    {
        return _transactions[index];
    }

    public string PrintStatus(bool status) => status == true ? "Completed" : "Failed";
}

