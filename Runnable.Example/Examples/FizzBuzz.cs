using System;
using Runnable.Attributes;

namespace Runnable.Example
{
    internal class FizzBuzz
    {
        [Runnable("Print FizzBuzz", MethodParameters = new object[] { 100 })]
        public static void Run(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                string output = "";

                if (i % 3 == 0)
                    output += "Fizz";

                if (i % 5 == 0)
                    output += "Buzz";

                if (string.IsNullOrEmpty(output))
                    output = i.ToString();

                Console.WriteLine(output);
            }
        }
    }
}
