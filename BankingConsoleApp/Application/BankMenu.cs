using BankApplication.Domain;
using BankApplication.Extensions;
using BankingConsoleApp.Extensions;

namespace BankApplication.Application;

public class BankMenu
{
    private readonly Dictionary<long, Account> _accounts = [];
    private readonly Dictionary<MainMenuOption, Action> _menuOptions;

    public BankMenu()
    {
        _menuOptions = new Dictionary<MainMenuOption, Action>
        {
            { MainMenuOption.AccessAccount, AccessAccount },
            { MainMenuOption.OpenAccount, OpenAccount },
            { MainMenuOption.Quit, QuitApplication }
        };
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            DisplayMenu();

            if (!TryGetSelectedOption(out MainMenuOption selectedOption))
            {
                Console.WriteLine("Invalid input. Select a valid option.");
                Console.ReadKey();
                continue;
            }

            ExecuteOption(selectedOption);
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine($"** {Bank.Name} **");

        foreach (var option in _menuOptions)
        {
            Console.WriteLine($"{(int)option.Key}. {option.Key.ToDisplayString()}");
        }

        Console.Write("\nSelection: ");
    }

    private bool TryGetSelectedOption(out MainMenuOption selectedOption)
    {
        return Enum.TryParse(Console.ReadLine(), out selectedOption) && _menuOptions.ContainsKey(selectedOption);
    }

    private void ExecuteOption(MainMenuOption selectedOption)
    {
        _menuOptions[selectedOption].Invoke();
    }

    private void AccessAccount()
    {

        Console.WriteLine("Accessing account...");

        var accountNumber = ConsoleInput.GetLong("Enter account number: ");

        if (!_accounts.TryGetValue(accountNumber, out Account account))
        {
            Console.WriteLine("Account not found.");
            Console.ReadKey();
            return;
        }

        var pinNumber = ConsoleInput.GetPinNumber("Enter 4 digit PIN: ");

        if (account.Pin != pinNumber) 
        {
            Console.WriteLine("Access denied.");
            Console.ReadKey();
            return;
        }

        var accountMenu = new AccountMenu(account);

        accountMenu.Run();
    }

    private void OpenAccount()
    {
        Console.WriteLine("Opening account...");

        var account = AccountBuilder.Build();

        _accounts.Add(account.AccountNumber, account.Account);
    }

    private void QuitApplication()
    {
        Environment.Exit(0);
    }
}
