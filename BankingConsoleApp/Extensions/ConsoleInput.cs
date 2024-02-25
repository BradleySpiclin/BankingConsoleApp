using System.Text;
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

    public static long GetLong(string inputMessage)
    {
        while (true)
        {
            Console.Write(inputMessage);

            if (!long.TryParse(Console.ReadLine(), out var result))
            {
                Console.WriteLine("Error: Please enter an integer value.");
                continue;
            }

            return result;
        }
    }

    public static int GetPinNumber(string message)
    {
        Console.Write(message);

        try
        {
            var pinString = new StringBuilder();

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (char.IsDigit(key.KeyChar))
                {
                    pinString.Append(key.KeyChar);
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter || pinString.Length < 4);

            Console.WriteLine();

            if (int.TryParse(pinString.ToString(), out var pin))
            {
                return pin;
            }
            else
            {
                throw new FormatException("Invalid PIN format.");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
