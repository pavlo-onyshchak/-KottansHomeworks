using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata3
{
    public class PasswordVerifier
    {
        public void Verify(string password)
        {
            int size = password.Length;
            List<bool> conditions = new List<bool>();
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("empty string");
            }
            conditions.Add(true);//if string is not empty,than add true to the list
            conditions.Add(password.Any(x => Char.IsUpper(x)));
            conditions.Add(password.Any(x => Char.IsLower(x)));
            conditions.Add(password.Any(x => Char.IsNumber(x)));

            if (size > 8)
            {
                conditions.Add(true);
            }
            else
            {
                conditions.Add(false);
            }
            if (!conditions[2])
            {
                throw new Exception("password should have lowercase letter");
            }

            int counter = conditions.Count(x => x);
            if (counter < 3)
            {
                if (!conditions[4])
                {
                    throw new Exception("add upperrcase or  numbers to your password");
                }
                if (!conditions[1])
                {
                    throw new Exception("add uppercase letters or numbers or create password more than 8 chars");
                }
                if (!conditions[2])
                {
                    throw new Exception("add numbers or create password more than 8 chars");
                }
                if (!conditions[3])
                {
                    throw new Exception("add  uppercase letters or create password more than 8 chars");
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                string pass = "AAAAAAAAAAAAAAa";
                PasswordVerifier pas = new PasswordVerifier();
                pas.Verify(pass);
            }
        }
    }
}
