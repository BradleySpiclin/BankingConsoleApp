using System.Text.RegularExpressions;

namespace BankingConsoleApp.Extensions;

public class ConsoleInput
{
    public static decimal GetDecimal(string inputMessage)
    {
        while (true)
        {
            Console.Write(inputMessage);

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Error: Invalid input. Please enter a valid decimal number.");
                continue;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Error: Amount must be greater than 0.");
                continue;
            }

            return amount;
        }
    }

    public static string GetString(string inputMessage)
    {
        while (true)
        {
            Console.Write(inputMessage);
            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Error: Input cannot be null or empty.");
                continue;
            }

            if (!Regex.IsMatch(userInput, "^[a-zA-Z]*$"))
            {
                Console.WriteLine("Error: Input can only contain letters (a-z or A-Z).");
                continue;
            }

            return userInput;
        }
    }

    public static int GetInteger(string inputMessage)
    {
        while (true)
        {
            Console.Write(inputMessage);
            if (!int.TryParse(Console.ReadLine(), out int result))
            {
                Console.WriteLine("Error: Please enter an integer value.");
                continue;
            }

            return result;
        }
    }
}
