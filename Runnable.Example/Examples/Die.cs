using System;
using Runnable.Attributes;

namespace Runnable.Example
{
    internal class Die
    {
        Random _random = new Random();

        public Die(int seed)
        {
            _random = new Random(seed);
        }

        public int Roll()
        {
            return _random.Next(1, 7);
        }

        [Runnable("Roll 10 dice", ConstructorParameters = new object[] { 1337 })]
        public void TestRoll()
        {
            for (int i = 0; i < 10; i++)
                Console.WriteLine(Roll());
        }
    }
}
