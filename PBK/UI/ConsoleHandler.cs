using PBK.Entities;
using System;

namespace PBK.UI
{
    public class ConsoleHandler
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public string GetInput(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }

        public void ShowPassTime(Result result)
        {
            Console.WriteLine(TextForOutput.PassIsEnded);
            Console.WriteLine($"Pass time: {result.PassTime}");
        }

        public void ShowAnswersCorrectness(Result result)
        {
            Console.WriteLine($"Total correct answers: {result.CorrectAnswers}\n" +
                              $"Total incorrect answers: {result.IncorrectAnswers}");
        }

        public void ShowGrade(Result result)
        {
            Console.WriteLine($"Total grade: {result.Grade}");
        }

        public void ShowTopicStats(Topic topic)
        {
            topic.IncludedTests.ForEach(el =>
            {
                ShowMessage(el.ToString());
            });
        }
    }
}