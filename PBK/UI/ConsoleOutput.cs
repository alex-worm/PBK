using PBK.Entities;
using System;

namespace PBK.UI
{
    public class ConsoleOutput
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string GetInput(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        public void PrintTimeResult(Result result)
        {
            Console.WriteLine(TextForOutput.PassEnd);
            Console.WriteLine($"Pass time: {result.PassTime}");
        }

        public void PrintAnswersCorrectness(Result result)
        {
            Console.WriteLine($"Total correct answers: {result.CorrectAnswers}\n" +
                              $"Total incorrect answers: {result.IncorrectAnswers}");
        }

        public void PrintGrade(Result result)
        {
            Console.WriteLine($"Total grade: {result.Grade}");
        }

        public void PrintTopicStats(Topic topic)
        {
            topic.IncludedTests.ForEach(el =>
            {
                PrintMessage($"{el.Name} {el.PassesNumber} {el.TotalCorrectAnswers} {el.TotalIncorrectAnswers}");
            });
        }
    }
}