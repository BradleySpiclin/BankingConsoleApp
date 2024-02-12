using System.Text.RegularExpressions;

namespace BankingConsoleApp.Extensions;

public class ConsoleInput
{

    public static decimal GetDecimal(string inputMessage)
    {
        while (true)
        {
            Console.Write(inputMessage);

            var userInput = Console.ReadLine();

            if (!decimal.TryParse(userInput, out decimal amount))
                Console.WriteLine($"Error: '{userInput}' is not a valid amount");

            else if (amount <= 0)
                Console.WriteLine($"Amount must be greater than 0");

            else
                return amount;
        }
    }

    public static string GetString(string inputMessage)
    {
        while (true)
        {
            Console.Write(inputMessage);
            var userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
                Console.WriteLine("Input cannot be null or empty");

            else if (!Regex.IsMatch(userInput, "^[a-zA-Z]*$"))
                Console.WriteLine("Input can only contain letters (a-z or A-Z)");

            else
                return userInput;
        }
    }

    public static int GetInteger(string inputMessage)
    {
        while (true)
        {
            Console.Write(inputMessage);

            var userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out int result))
                Console.WriteLine("Please enter an integer value");
            else
                return result;
        }
    }
}
