using PBK.Test_setup;
using System;

namespace PBK.Logic
{
    public class Writer
    {
        public static void GetStarted()
        {
            ExecuteCommand("Hello friend.................\nEnter your command:");            
        }

        private static void ExecuteCommand(string request)
        {
            switch (request)
            {
                case "add":
                    TestCreator.CreateNewTest(DataEntry("Enter the name for the new test:"));
                    DataEntry("Your test is created. To continue press Enter..");
                    Console.Clear();
                    break;

                case "edit":
                    Console.WriteLine("Enter test name to edit:");
                    Console.Clear();
                    break;

                case "delete":
                    Console.WriteLine("Enter test name to delete:");
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
