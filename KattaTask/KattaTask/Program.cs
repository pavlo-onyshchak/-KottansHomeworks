using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Permissions;
using System.Diagnostics;


namespace KattaTask
{


    public class Calculator
    {

        ILogger logger = null;
        IWebservice web_service = null;
        public Calculator(ILogger log, IWebservice web)
        {
          logger = log;
          web_service = web;
        }

        public int Add(string numbers)
        {
            int result = 0;
            try
            {
                result = AddInternal(numbers);
                
            }
            catch(IOException e)
            {
                web_service.show();
            }
            return result;
        }

        private int AddInternal(string numbers)
        {
          
            logger.Write(numbers, "started");
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
                    int length = (index - 1) - 2;
                    var only_delimiters = numbers.Substring(2, length);

                    delimiters = only_delimiters.Split(brackets, StringSplitOptions.RemoveEmptyEntries);
                    string without_delimiters = numbers.Substring(index); //string after [];
                    digits = without_delimiters.Split(delimiters, StringSplitOptions.None);
                }

                
                List<string> not_valid_numbers = new List<string>();
                int sum = 0;
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
            logger.Write(numbers, sum.ToString());
            
            return sum;
        }

    }

    public interface ILogger
    {
        void Write(string message, string result);
    }

    public class ILog : ILogger
    {
        public void Write(string message, string result)
        {
            string path = Path.GetFullPath(@"E:\-KottansHomeworks\KattaTask\KattaTask\log.txt");

            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                streamWriter.WriteLine("the string is - " + message + " , the result is: " + result);
                streamWriter.Close();

            }

        }
    }

    public class BrokenLoger : ILogger //using for creating logger exception
    {
        public void Write(string message, string result)
        {
            throw new IOException();
        }
    }

    public interface IWebservice
    {
        void show();
    }

    public class IWeb : IWebservice
    {
        public void show()
        {
            Console.WriteLine("loggers exception");
        }
    }




   class Program
    {
        static void Main(string[] args)
        {
            ILogger obj = new ILog();
            ILogger obj1 = new BrokenLoger();
            IWebservice web = new IWeb();
            Calculator calc = new Calculator(obj,web);
           
            if (args.Length != 1)
            {
                Console.WriteLine("exactly one parameter");
                return;
            }

            while (args[0].Length != 0)
            {
                Console.WriteLine("your input is - " + args[0]);
                Console.WriteLine("The result is - " + calc.Add(args[0]));
                Console.WriteLine("Next input");
                args[0] = Console.ReadLine();
            }



        }
    }
}
