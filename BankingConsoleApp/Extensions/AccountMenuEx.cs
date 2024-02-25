using BankApplication.Application;

namespace BankApplication.Extensions;

public static class AccountMenuEx
{
    public static string ToDisplayString(this AccountMenuOption option)
    {
        return option switch
        {
            AccountMenuOption.Deposit => "Deposit Funds",
            AccountMenuOption.Withdraw => "Withdraw Funds",
            AccountMenuOption.Transfer => "Transfer Funds",
            AccountMenuOption.Balance => "Display Balance",
            AccountMenuOption.TransactionRecord => "Display Transaction History",
            AccountMenuOption.MainMenu => "Return to Main Menu",
            AccountMenuOption.Quit => "Quit",
            _ => option.ToString()
        };
    }
}
