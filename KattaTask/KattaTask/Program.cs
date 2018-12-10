using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KattaTask
{
    public class Calculator
    {
        public static int Add(string numbers)
        {
            if (String.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            string[] delimiters = new string[] { ",", "\n" };
            string[] digits = numbers.Split(delimiters, StringSplitOptions.None);

            if (numbers.StartsWith("//"))
            {
                string[] brackets = { "]", "[" };
                int index = numbers.IndexOfAny("0123456789".ToCharArray());
                int length = (index-1) - 2;
                var only_delimiters = numbers.Substring(2, length);

                delimiters = only_delimiters.Split(brackets, StringSplitOptions.RemoveEmptyEntries);
                string without_delimiters = numbers.Substring(index); //string after [];
                digits = without_delimiters.Split(delimiters, StringSplitOptions.None);
            }

            int sum = 0;
            List<string> not_valid_numbers = new List<string>();

            foreach (var element in digits)
            {
                int value = Int32.Parse(element);
                if (value < 0)
                {
                    not_valid_numbers.Add(element);
                    continue;
                }

                if (value > 1000)
                {
                    continue;
                }

                sum += value;
            }

            if (not_valid_numbers.Count > 0)
            {
                throw new ArgumentException("negatives not allowed " + string.Join(",", not_valid_numbers));
            }

            return sum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Calculator.Add("//;\n1;2"));
            
        }
    }
}
