using PBK.Entities;
using System;
using System.Threading;

namespace PBK.UI
{
    public class ConsoleOutput
    {
        public void DisplayTopicInfo(Topic topic)
        {
            foreach(var i in topic.IncludedTests)
            {
                Console.WriteLine($"{i.Name} {i.PassesNumber} {i.TotalCorrectAnswers} {i.QuestionsNumber * i.PassesNumber - i.TotalCorrectAnswers}");
            }
        }

        public string DataEntry(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        public void ShowResults(Result results, bool gradeAvailability)
        {
            Console.WriteLine($"Total correct answers: {results.CorrectAnswers}\n" +
                $"Total incorrect answers: {results.IncorrectAnswers}");
            if (gradeAvailability)
            {
                Console.WriteLine($"Total grade: {results.Grade}");
            }
        }
    }
}
