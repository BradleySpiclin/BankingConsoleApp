using BankingConsoleApp.Extensions;

namespace BankingConsoleApp;

public enum MenuOption
{
    AddNewAccount = 1,
    Deposit,
    Withdraw,
    Transfer,
    Print,
    TransactionHistory,
    Quit
}

public class BankSystem
{
    public const string AccountNotFound = "Account does not exist.";

    static void Main(string[] args)
    {
        var bank = new Bank();

        while (true)
        {
            MenuOption selectedOption = ReadUserOption();
            switch (selectedOption)
            {
                case MenuOption.AddNewAccount:
                    AddNewAccount(bank);
                    break;
                case MenuOption.Deposit:
                    DoDeposit(bank);
                    break;
                case MenuOption.Withdraw:
                    DoWithdraw(bank);
                    break;
                case MenuOption.Transfer:
                    DoTransfer(bank);
                    break;
                case MenuOption.Print:
                    DoPrint(bank);
                    break;
                case MenuOption.TransactionHistory:
                    DoRollback(bank);
                    break;
                case MenuOption.Quit:
                    return;
                default:
                    break;
            }
            Console.Write("\nPress any key...");

            var readKey = Console.ReadKey().KeyChar;

            Console.Clear();
        }
    }

    public static MenuOption ReadUserOption()
    {
        MenuOption userChoice;
        do
        {
            DisplayMenuOptions();
            var userInput = Console.ReadLine();
            if (Enum.TryParse(userInput, out userChoice) &&
                Enum.IsDefined(typeof(MenuOption), userChoice))
            {
                break;
            }
            else
            {
                Console.WriteLine($"Invalid input '{userInput}'. Select a valid option.");
            }
        } while (true);

        return userChoice;
    }

    private static void DisplayMenuOptions()
    {
        Console.WriteLine("** Banking System Main Menu **");
        Console.WriteLine($"{(int)MenuOption.AddNewAccount}. Add new account");
        Console.WriteLine($"{(int)MenuOption.Deposit}. Deposit funds");
        Console.WriteLine($"{(int)MenuOption.Withdraw}. Withdraw funds");
        Console.WriteLine($"{(int)MenuOption.Transfer}. Transfer funds");
        Console.WriteLine($"{(int)MenuOption.Print}. Print account");
        Console.WriteLine($"{(int)MenuOption.TransactionHistory}. Transaction history");
        Console.WriteLine($"{(int)MenuOption.Quit}. Exit program");
        Console.Write("Selection: ");
    }

    public static bool ConfirmMenuSelection(string message)
    {
        Console.Write(message);
        char input = Console.ReadKey().KeyChar;
        return input == 'y' || input == 'Y';
    }

    private static void AddNewAccount(Bank bank)
    {
        var newAccount = new Account(ConsoleInput.GetString("Account name: "), ConsoleInput.GetDecimal("Opening balance: "));
        bank.AddAccount(newAccount);
        Console.WriteLine($"New account: {newAccount.Name} created.");
    }

    private static Account FindAccount(Bank bank, string prompt)
    {
        var accountName = ConsoleInput.GetString(prompt);
        var accountSearch = bank.GetAccount(accountName);
        return accountSearch;
    }

    private static void DoDeposit(Bank bank)
    {
        var bankAccount = FindAccount(bank, "Account name: ");

        if (bankAccount != null)
        {
            var depositAmount = ConsoleInput.GetDecimal("Enter deposit amount: ");

            var depositTransaction = new DepositTransaction(bankAccount, depositAmount);

            try
            {
                bank.ExecuteTransaction(depositTransaction);
                depositTransaction.Print();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        else
        {
            Console.WriteLine(AccountNotFound);
        }
    }

    private static void DoWithdraw(Bank bank)
    {
        var bankAccount = FindAccount(bank, "Account name: ");

        if (bankAccount != null)
        {
            var withdrawAmount = ConsoleInput.GetDecimal("Enter withdraw amount: ");
            var withdrawTransaction = new WithdrawTransaction(bankAccount, withdrawAmount);

            try
            {
                bank.ExecuteTransaction(withdrawTransaction);
                withdrawTransaction.Print();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        else
        {
            Console.WriteLine(AccountNotFound);
        }
    }

    private static void DoTransfer(Bank bank)
    {
        var fromAccount = FindAccount(bank, "Transfer from account: ");

        if (fromAccount == null)
        {
            Console.WriteLine(AccountNotFound);
            return;
        }

        var toAccount = FindAccount(bank, "Transfer to account: ");

        if (toAccount == null)
        {
            Console.WriteLine(AccountNotFound);
            return;
        }

        var transferAmount = ConsoleInput.GetDecimal("Enter transfer amount: ");

        var transferTransaction = new TransferTransaction(fromAccount, toAccount, transferAmount);

        try
        {
            bank.ExecuteTransaction(transferTransaction);

            if (transferTransaction.Success)
            {
                transferTransaction.Print();
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    private static void DoPrint(Bank bank)
    {
        var bankAccount = FindAccount(bank, "Account name: ");

        if (bankAccount != null)
            bankAccount.Print();
        else
            Console.WriteLine("Account does not exist.");
    }

    private static void DoRollback(Bank bank)
    {
        bank.PrintTransactionHistory();

        if (ConfirmMenuSelection("Rollback transaction (Y/N): "))
        {
            int index = ConsoleInput.GetInteger("\nSelect transaction number #: ") - 1;
            try
            {
                var transaction = bank.GetTransactionAtIndex(index);
                bank.RollbackTransaction(transaction);
                Console.WriteLine("Rollback successful");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
