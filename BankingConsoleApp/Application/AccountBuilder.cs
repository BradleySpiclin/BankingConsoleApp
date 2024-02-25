using BankApplication.Domain;
using BankingConsoleApp.Extensions;

namespace BankApplication.Application;

public static class AccountBuilder
{
    public static int AccountNumber = 1;
    public static (long AccountNumber, Account Account) Build() 
    {
        var firstName = ConsoleInput.GetString("First name: ");
        var lastName = ConsoleInput.GetString("Last name: ");

        int firstPin, secondPin;
        do
        {
            firstPin = ConsoleInput.GetPinNumber("Enter 4 digit PIN: ");
            secondPin = ConsoleInput.GetPinNumber("Confirm 4 digit PIN: ");

            if (secondPin != firstPin)
            {
                Console.WriteLine("PINs do not match... Please try again.");
            }

        } while (secondPin != firstPin);


        var openingBalance = ConsoleInput.GetDecimal("Opening balance: ");

        var account = new Account(firstName, lastName, openingBalance, firstPin);

        var accountNumber = AccountNumber;

        AccountNumber++;

        Console.WriteLine($"New account created.\n{firstName} {lastName}.Account Number: {accountNumber}\nBalance: {openingBalance:C}");

        Console.ReadKey(true);

        return (accountNumber, account);
    }
    private static long GenerateAccountNumber()
    {
        Random random = new();
        return random.Next(100000000, 1000000000);
    }
}
