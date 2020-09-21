using PBK.Entities;
using PBK.Logic.TestEditing;
using PBK.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PBK.Logic.TestPassing
{
    class Tester
    {
        private const int millisecsInMinute = 6000;

        private readonly CancellationTokenSource cancellToken = new CancellationTokenSource();
        private readonly Stopwatch stopwatch = new Stopwatch();

        public void OpenTest(string name)
        {
            var serializator = new TestSerializator();
            var writer = new ConsoleOutput();
            var test = serializator.Deserialize(name);
            var results = new Result();
            var passAnswers = new List<string>();

            if (test == null)
            {
                Console.WriteLine(TextForOutput.notOpened);
                return;
            }

            cancellToken.CancelAfter(test.TimerValue * millisecsInMinute);

            PassTest(test, results, passAnswers);

            test.PassesNumber++;
            test.TotalCorrectAnswers += results.CorrectAnswers;
            test.TotalIncorrectAnswers += results.IncorrectAnswers;

            using (StreamWriter fileWriter = new StreamWriter($@"{test.Name}({test.PassesNumber}).txt"))
            {
                for (var i = 0; i < results.CorrectAnswers + results.IncorrectAnswers; i++)
                {
                    fileWriter.WriteLine($"{test.Questions[i].QuestionText}: {passAnswers[i]}");
                }
            }         

            writer.ShowResults(results, test.ClosedQuestions, test.GradeAvailability, stopwatch.Elapsed);

            serializator.Serialize(test);                
        }

        private void PassTest(Test test, Result result, List<string> passAnswers)
        {
            stopwatch.Start();

            test.Questions.ForEach(el =>
            {
                if (cancellToken.IsCancellationRequested)
                {
                    return;
                }

                var userAnswer = AnswerTheQuestion(test, el);

                if (userAnswer == el.CorrectAnswer)
                {
                    result.CorrectAnswers++;
                    result.Grade += el.QuestionRating;
                }
                else
                {
                    result.IncorrectAnswers++;
                }

                passAnswers.Add(userAnswer);

                if (test.IndicateCorrectAnswer)
                {
                    Console.WriteLine(userAnswer == el.CorrectAnswer);
                }
            });

            stopwatch.Stop();
        }

        private string AnswerTheQuestion(Test test, Question question)
        {
            Console.WriteLine(question.QuestionText);

            for (var i = 0; i < question.AnswersNumber; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Answers[i]}");
            }

            return Console.ReadLine();
        }
    }
}
