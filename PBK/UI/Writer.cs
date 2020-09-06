using PBK.Test_setup;
using System;

namespace PBK.UI
{
    public class Writer
    {
        public static void GetStarted()
        {
            Console.WriteLine(TextForOutput.greeting);

            ExecuteCommand(DataEntry(TextForOutput.enterCommand));            
        }

        private static void ExecuteCommand(string request)
        {
            if(!int.TryParse(request, out int result))
            {
                Console.WriteLine("\aWrong Input");

                ExecuteCommand(DataEntry(TextForOutput.enterCommand));
            }

            switch (result)
            {
                case (int)Command.Add:
                    TestTool.CreateNewTest(DataEntry(TextForOutput.nameToAdd));
                    Console.Clear();
                    break;

                case (int)Command.Edit:
                    TestTool.EditTest(DataEntry(TextForOutput.nameToEdit));
                    Console.Clear();
                    break;

                case (int)Command.Delete:
                    TestTool.DeleteTest(DataEntry(TextForOutput.nameToDelete));
                    Console.Clear();
                    break;

                case (int)Command.Open:
                    TestTool.OpenTest(DataEntry(TextForOutput.nameToOpen));
                    Console.Clear();
                    break;
            }

            ExecuteCommand(DataEntry(TextForOutput.enterCommand));
        }

        public static string DataEntry(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        public static void ShowResult(object test)
        {
            Console.WriteLine($"Your result is: ");
        }
    }
}
