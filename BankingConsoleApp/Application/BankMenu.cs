using BankApplication.Domain;
using BankApplication.Extensions;

namespace BankApplication.Application;

public class BankMenu
{
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

        Console.Write("Selection: ");
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
    }

    private void OpenAccount()
    {
        Console.WriteLine("Opening account...");
    }

    private void QuitApplication()
    {
        Environment.Exit(0);
    }
}
