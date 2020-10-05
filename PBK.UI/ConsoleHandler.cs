using System;
using Common;

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

        internal int GetIntValue(string message)
        {
            int result;

            while (!int.TryParse(GetInput(message), out result) || result < 0)
            {
                Console.WriteLine(TextForOutput.IncorrectInput);
            }

            return result;
        }

        internal bool GetBoolValue(string message)
        {
            int result;

            while (!int.TryParse(GetInput(message),
                out result) || result != 1 && result != 2)
            {
                Console.WriteLine(TextForOutput.IncorrectInput);
            }

            return result == 1;
        }
    }
}
