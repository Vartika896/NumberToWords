using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

public static class Solution
{
    public static string NumbersToWords(decimal value)
    {
        string strWords = "";
        string input = Math.Round(value, 2).ToString();
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            input = input.Remove(input.IndexOf("."));
        }

        strWords = GetNumbersToWords(input) + " Dollars";

        if (decimals.Length > 0)
        {
            strWords += " and " + GetNumbersToWords(decimals) + " Cents";
        }


        return strWords;
    }

    private static string GetNumbersToWords(string input)
    {
        // these are seperators for each 3 digit in numbers. you can add more if you want convert beigger numbers.
        string[] seperators = { "", " Thousand ", " Million " };

        // Counter is indexer for seperators. each 3 digit converted this will count.
        int i = 0;

        string strWords = "";

        while (input.Length > 0)
        {
            string _digits = input.Length < 3 ? input : input.Substring(input.Length - 3);

            input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

            int num = int.Parse(_digits);

            _digits = GetWord(num);

            if (_digits != "")
            {
                _digits += seperators[i];
            }

            strWords = _digits + strWords;

            i++;
        }
        return Regex.Replace(strWords, @"\s+", " ");
    }
    private static string GetWord(int numb)
    {
        string[] Ones =
        {
            "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

        string[] Tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        string word = "";

        if (numb > 99 && numb < 1000)
        {
            int i = numb / 100;
            word = word + Ones[i - 1] + " Hundred ";
            numb = numb % 100;
        }

        if (numb > 19 && numb < 100)
        {
            int i = numb / 10;
            word = word + Tens[i - 1] + " ";
            numb = numb % 10;
        }

        if (numb > 0 && numb < 20)
        {
            word = word + Ones[numb - 1];
        }

        return word;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        decimal numbers;
        string? decimalNumb = Console.ReadLine();

#pragma warning disable CS8604 // Possible null reference argument.
        decimalNumb = String.Concat(decimalNumb.Where(c => !Char.IsWhiteSpace(c))).Replace(".", "");
#pragma warning restore CS8604 // Possible null reference argument.

        if (decimal.TryParse(decimalNumb, NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture("de-DE"), out numbers))
        {
            if (numbers <= 999999999.99M)
            {
                Console.WriteLine("convert numbers to words: " + Solution.NumbersToWords(numbers));
            }
            else
            {
                Console.WriteLine("Max number is 999999999.99");
            }
        }
        else
        {
            Console.WriteLine("Unable to convert");
        }

    }
}