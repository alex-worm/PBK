using PBK.Test_setup;
using System;
using System.Collections.Generic;
using System.Text;

namespace PBK.Logic
{
    class Writer
    {
        protected static void Welcome()
        {
            Console.WriteLine("Hello friend.................");
        }

        protected static void CommandExecute(string request)
        {
            while (true)
            {
                switch (request)
                {
                    case "add":
                        TestCreator.CreateNewTest(DataEntry("Enter the name for the new test:"));
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
                        Console.Clear();
                        continue;
                }
            }
        }

        internal static string DataEntry(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        //internal static void ShowResult()
        //{
        //
        //}
    }
}
