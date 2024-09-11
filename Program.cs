using System;
using System.Text.RegularExpressions;
using System.Data;

public class MathExpressionValidatorCalculator
{
    public static void Main()
    {
        // Initialize variables
        string input;
        string separator = "--------------------------------------------------------------------";
        bool isValid = false;

        // Regular expression to check that the input only contains numbers and mathematical operators
        string pattern = @"^[0-9+\-*/\s]+$";

        // Regular expression to find double '/' or '*'
        string doubleOpPattern = @"[/|*]{2}";

        // Regular expression to check if there is an operator at the beginning or end of the string
        string opStartEndPattern = @"^[/|*+\-]|[*/+-]$";

        // The program will repeat until the operation meets the requirements
        while (!isValid)
        {
            // Prompt the user to input the expression as a string
            Console.WriteLine("Enter your operation to calculate:");
            Console.WriteLine(separator);
            input = Console.ReadLine() ?? string.Empty;

            if (Regex.IsMatch(input, pattern))
            {
                // Check if the input contains '*' immediately next to '/' or vice versa (*/ or /*)
                if (input.Contains("*/") || input.Contains("/*"))
                {
                    Console.WriteLine("There cannot be a '/' next to a '*', or a '*' next to a '/'. Try again.");
                }
                // Check if the input contains double mathematical operators (like "//" or "**")
                else if (Regex.IsMatch(input, doubleOpPattern))
                {
                    Console.WriteLine("The input contains double mathematical operators. Try again.");
                    Console.WriteLine(separator);
                }
                // Check if the input contains operators at the beginning or end
                else if (Regex.IsMatch(input, opStartEndPattern))
                {
                    Console.WriteLine("The input contains an operator at the beginning or end of the string. Try again.");
                    Console.WriteLine(separator);
                }
                // Check if the input contains spaces
                else if (input.Contains(" "))
                {
                    Console.WriteLine("Spaces are not allowed. Try again.");
                }
                // Check if the input contains divisions by zero
                else if (input.Contains("/0"))
                {
                    Console.WriteLine("Division by zero is not allowed. Try again.");
                }
                // If all checks pass, perform the calculation
                else
                {
                    string value = new DataTable().Compute(input, null)?.ToString() ?? "0";
                    Console.WriteLine("The result is: " + value);
                    isValid = true;
                }
            }
            // Check if the input contains only permitted characters
            else
            {
                Console.WriteLine("The input contains invalid characters. Try again.");
                Console.WriteLine(separator);
            }
        }
    }
}
