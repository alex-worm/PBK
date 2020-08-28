using PBK.Test_setup;
using System;

namespace PBK.Logic
{
    public class Writer
    {
        public static void GetStarted()
        {
            ExecuteCommand(DataEntry("Hello friend.................\nEnter your command:"));            
        }

        private static void ExecuteCommand(string request)
        {
            switch (request)
            {
                case "add":
                    TestTool.CreateNewTest(DataEntry("Enter the name for the new test:"));
                    DataEntry("Your test is created. To continue press Enter..");
                    Console.Clear();
                    break;

                case "edit":
                    TestTool.EditTest(DataEntry("Enter test's name to edit it: "));
                    DataEntry("Your test is changed. To continue press Enter..");
                    Console.Clear();
                    break;

                case "delete":
                    DataEntry($"{TestTool.DeleteTest(DataEntry("Enter test name to delete:"))}" +
                        $"\nTo continue press Enter..");
                    Console.Clear();
                    break;

                case "open":
                    Console.WriteLine("Enter test name to open:");
                    Console.Clear();
                    break;

                default:
                    Console.WriteLine("Command not recognized. Try again:");
                    break;
            }

            ExecuteCommand(DataEntry("Enter your command:"));
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
