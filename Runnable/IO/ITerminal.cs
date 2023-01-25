namespace Runnable.IO
{
    /// <summary>
    /// Specifies the types of messages that can be printed in an <see cref="ITerminal"/>.
    /// </summary>
    public enum MessageType { Text, Error }

    /// <summary>
    /// Represents a terminal, that <see cref="Runner"/> can use to get user input and display text.
    /// </summary>
    public interface ITerminal
    {
        /// <summary>
        /// Prints a message to the terminal.
        /// </summary>
        /// <param name="message">The content of the message.</param>
        /// <param name="type">The type of the message.</param>
        void PrintMessage(string message, MessageType type);

        /// <summary>
        /// Clears the terminal of all text.
        /// </summary>
        void Clear();

        /// <summary>
        /// Reads the next text input from the terminal, and returns a value indicating whether the input was a valid number.
        /// </summary>
        /// <param name="min">The smallest valid value.</param>
        /// <param name="max">The largest valid value.</param>
        /// <param name="value">Contains the number that was entered in the terminal if the input was valid,
        /// or 0 if the input was invalid.</param>
        /// <returns>true if the input was valid, otherwise false.</returns>
        bool ReadInt(int min, int max, out int value);
    }
}
