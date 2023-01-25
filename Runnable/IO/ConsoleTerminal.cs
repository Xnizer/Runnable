using System;

namespace Runnable.IO
{
    internal class ConsoleTerminal : ITerminal
    {
        public void PrintMessage(string message, MessageType type)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            switch (type)
            {
                case MessageType.Text:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case MessageType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    Console.ForegroundColor = originalColor;
                    break;
            }

            Console.WriteLine(message);

            Console.ForegroundColor = originalColor;
        }

        public void Clear()
        {
            Console.Clear();
        }

        public bool ReadInt(int min, int max, out int value)
        {
            while (true)
            {
                var keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Escape)
                    break;

                if (int.TryParse(keyInfo.KeyChar.ToString(), out value))
                    if (value >= min && value <= max)
                        return true;
            }

            value = 0;
            return false;
        }
    }
}
