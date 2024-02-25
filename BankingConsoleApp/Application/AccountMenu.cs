using BankApplication.Domain;
using BankApplication.Extensions;

namespace BankApplication.Application;

public class AccountMenu
{
    private readonly Dictionary<AccountMenuOption, Action> _menuOptions;
    private readonly Account _account;

    public AccountMenu(Account account)
{
        _menuOptions = new Dictionary<AccountMenuOption, Action>
        {
            { AccountMenuOption.Deposit, DepositFunds },
            { AccountMenuOption.Withdraw, WithdrawFunds },
            { AccountMenuOption.Transfer, TransferFunds },
            { AccountMenuOption.Balance, DisplayBalance },
            { AccountMenuOption.TransactionRecord, ShowRecords },
            { AccountMenuOption.Quit, Quit }
        };
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            DisplayMenu();

            if (!TryGetSelectedOption(out AccountMenuOption selectedOption))
            {
                Console.WriteLine("Invalid input. Select a valid option.");
                Console.ReadKey();
                continue;
            }

            ExecuteOption(selectedOption);
        }
    }

    public void DisplayMenu()
    {
        Console.WriteLine($"** Account Menu **");

        foreach (var option in _menuOptions)
        {
            Console.WriteLine($"{(int)option.Key}. {option.Key.ToDisplayString()}");
        }

        Console.WriteLine("\nSelection: ");
    }

    private bool TryGetSelectedOption(out AccountMenuOption selectedOption)
    {
        return Enum.TryParse(Console.ReadLine(), out selectedOption) && _menuOptions.ContainsKey(selectedOption);
    }

    private void ExecuteOption(AccountMenuOption selectedOption)
    {
        _menuOptions[selectedOption].Invoke();
    }

    private static void DepositFunds() { }
    private void WithdrawFunds() { }
    private void TransferFunds() { }
    private void DisplayBalance() { }
    private void ShowRecords() { }
    private void MainMenu() 
    {
        return;
    }

    private void Quit() 
    {
        Environment.Exit(0);
    }
}
