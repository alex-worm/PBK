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

        public void ShowTimeResult(Result result)
        {
            Console.WriteLine(TextForOutput.passEnd);
            Console.WriteLine($"Pass time: {result.PassTime}");
        }

        public void ShowCorrectnessOfQAnswers(Result result)
        {
            Console.WriteLine($"Total correct answers: {result.CorrectAnswers}\n" +
                $"Total incorrect answers: {result.IncorrectAnswers}");
        }

        public void ShowGrade(Result result)
        {
            Console.WriteLine($"Total grade: {result.Grade}");
        }
    }
}
