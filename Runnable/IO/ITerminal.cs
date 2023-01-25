namespace Runnable.IO
{
    public enum MessageType { Text, Error }
    public interface ITerminal
    {
        void PrintMessage(string message, MessageType type);
        void Clear();
        bool ReadInt(int min, int max, out int value);
    }
}
