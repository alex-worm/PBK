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

        public static void ExecuteCommand(string request)
        {
            if(!int.TryParse(request, out int result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);

                ExecuteCommand(DataEntry(TextForOutput.enterCommand));
            }

            switch (result)
            {
                case (int)Command.Add:
                    TestTool.CreateNewTest();
                    break;

                case (int)Command.Edit:
                    TestTool.EditTest();
                    break;

                case (int)Command.Delete:
                    TestTool.DeleteTest(DataEntry(TextForOutput.nameToDelete));
                    break;

                case (int)Command.Open:
                    TestTool.OpenTest();
                    break;

                case (int)Command.Exit:
                    Environment.Exit(0);
                    break;
            }

            ExecuteCommand(DataEntry(TextForOutput.enterCommand));
        }

        public static string DataEntry(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }
    }
}
