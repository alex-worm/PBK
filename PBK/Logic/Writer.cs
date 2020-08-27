using PBK.Test_setup;
using System;
using System.Collections.Generic;
using System.Text;

namespace PBK.Logic
{
    class Writer
    {
        internal static void Welcome()
        {
            Console.WriteLine("Hello friend.................");

            while (true)
            {
                CommandExecute(DataEntry("Enter your command:"));
            }
            
        }

        private static void CommandExecute(string request)
        {
            switch (request)
            {
                case "add":
                    TestCreator.CreateNewTest(DataEntry("Enter the name for the new test:"));
                    DataEntry("Your test is created. To continue press Enter..");
                    Console.Clear();
                    return;
                case "edit":
                    Console.WriteLine("Enter test name to edit:");
                    Console.Clear();
                    return;
                case "delete":
                    Console.WriteLine("Enter test name to delete:");
                    Console.Clear();
                    return;
                case "open":
                    Console.WriteLine("Enter test name to open:");
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Command not recognized. Try again:");
                    return;
            }
        }

        internal static string DataEntry(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        internal static void ShowResult()
        {
            Console.WriteLine($"Your result is: ...");
        }
    }
}
