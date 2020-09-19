using PBK.Entities;
using PBK.Logic.EntityEditing;
using PBK.Logic.TestPassing;
using System;
using System.Threading;

namespace PBK.UI
{
    public class Writer
    {
        private const int millisecsInMinute = 60000;

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
                    Tester.PassTest(DataEntry(TextForOutput.nameToOpen));
                    break;

                case (int)Command.Exit:
                    Environment.Exit(0);
                    break;
            }

            ExecuteCommand(DataEntry(TextForOutput.enterCommand));
        }

        public static void DisplayTopicInfo(Topic topic)
        {
            foreach(var i in topic.IncludedTests)
            {
                Console.WriteLine($"{i.TestName} {i.PassesNumber} {i.TotalCorrectAnswers} {i.QuestionsNumber * i.PassesNumber - i.TotalCorrectAnswers}");
            }
        }

        public static string DataEntry(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        public static void ShowResult(object obj)
        {
            var test = (Test)obj;
            Thread.Sleep(test.TimerValue * millisecsInMinute);

            Console.WriteLine("\aTime's out");
            test.PassesNumber++;
        }
    }
}
