using PBK.Test_setup;
using System;

namespace PBK.Logic
{
    public class Writer
    {
        public static void GetStarted()
        {
            ExecuteCommand(DataEntry("Hello friend.................\nEnter your command:\n1. Add\n2. Edit\n3. Delete\n4. Open\n"));            
        }

        private static void ExecuteCommand(string request)
        {
            if(!int.TryParse(request, out int result))
            {
                Console.WriteLine("Command not recognized. Try again:");
                ExecuteCommand(DataEntry("Enter your command:"));
            }

            switch (result)
            {
                case (int)Command.Add:
                    TestTool.CreateNewTest(DataEntry("Enter the name for the new test:"));
                    DataEntry("Your test is created. To continue press Enter..");
                    Console.Clear();
                    break;

                case (int)Command.Edit:
                    TestTool.EditTest(DataEntry("Enter test's name to edit it: "));
                    DataEntry("Your test is changed. To continue press Enter..");
                    Console.Clear();
                    break;

                case (int)Command.Delete:
                    DataEntry($"{TestTool.DeleteTest(DataEntry("Enter test name to delete:"))}" +
                        $"\nTo continue press Enter..");
                    Console.Clear();
                    break;

                case (int)Command.Open:
                    Console.WriteLine("Enter test name to open:");
                    Console.Clear();
                    break;
            }

            ExecuteCommand(DataEntry("Enter your command:\n1. Add\n2. Edit\n3. Delete\n4. Open\n"));
        }

        public static string DataEntry(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        public static void ShowResult()
        {
            Console.WriteLine($"Your result is: ...");
        }
    }
}
