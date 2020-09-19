using PBK.Entities;
using System;
using System.Threading;

namespace PBK.UI
{
    public class ConsoleOutput
    {
        private const int millisecsInMinute = 60000;

        public void DisplayTopicInfo(Topic topic)
        {
            foreach(var i in topic.IncludedTests)
            {
                Console.WriteLine($"{i.TestName} {i.PassesNumber} {i.TotalCorrectAnswers} {i.QuestionsNumber * i.PassesNumber - i.TotalCorrectAnswers}");
            }
        }

        public string DataEntry(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        public void ShowResult(Test test)
        {
            Thread.Sleep(test.TimerValue * millisecsInMinute);

            test.Results.ForEach(el => Console.WriteLine(el));
        }
    }
}
