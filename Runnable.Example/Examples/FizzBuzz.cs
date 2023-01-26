using System.Text;
using Runnable.Attributes;

namespace Runnable.Example
{
    internal class FizzBuzz
    {
        [Runnable("Print FizzBuzz")]
        [MethodParameters(100)]
        public static string FizzBuzzString(int n)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 1; i <= n; i++)
            {
                if (i % 3 != 0 && i % 5 != 0)
                    builder.Append(i);
                else
                {
                    if (i % 3 == 0)
                        builder.Append("Fizz");

                    if (i % 5 == 0)
                        builder.Append("Buzz");
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
