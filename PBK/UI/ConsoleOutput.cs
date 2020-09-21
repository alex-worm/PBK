using PBK.Entities;
using System;

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

        public void ShowResults(Result results, bool closedQuestions, bool gradeAvailability, TimeSpan passTime)
        {
            Console.WriteLine(TextForOutput.passEnd);
            Console.WriteLine($"Pass time: {passTime}");

            if (closedQuestions)
            {
                Console.WriteLine($"Total correct answers: {results.CorrectAnswers}\n" +
                $"Total incorrect answers: {results.IncorrectAnswers}");
            }
            
            if (gradeAvailability)
            {
                Console.WriteLine($"Total grade: {results.Grade}");
            }
        }
    }
}
