using System;

namespace UI
{
    public class ConsoleHandler
    {
        internal void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        internal string GetInput(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }
    }
}