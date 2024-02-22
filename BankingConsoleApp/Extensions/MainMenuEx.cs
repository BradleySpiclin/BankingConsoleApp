using BankApplication.Application;

namespace BankApplication.Extensions;

public static class MainMenuEx
{
    public static string ToDisplayString(this MainMenuOption option)
    {
        return option switch
        {
            MainMenuOption.AccessAccount => "Access Account",
            MainMenuOption.OpenAccount => "Open Account",
            MainMenuOption.Quit => "Quit",
            _ => option.ToString()
        };
    }
}
