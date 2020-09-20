using PBK.Entities;
using PBK.Logic.TestEditing;
using PBK.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PBK.Logic.TestPassing
{
    class Tester
    {
        private const int millisecsInMinute = 60000;

        private static readonly CancellationTokenSource cancellToken = new CancellationTokenSource();

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

            try
            {
                cancellToken.CancelAfter(test.TimerValue * millisecsInMinute);

                PassTest(test, results, passAnswers);
            }
            finally
            {
                Console.WriteLine(TextForOutput.passEnd);

                test.PassesNumber++;
                test.TotalCorrectAnswers += results.CorrectAnswers;
                test.TotalIncorrectAnswers += results.IncorrectAnswers;

                if (!test.ClosedQuestions)
                {
                    for(var i = 0; i < test.QuestionsNumber; i++)
                    {
                        File.WriteAllText($@"{test.Name}({test.PassesNumber}).txt", $"{test.Questions[i]}: {passAnswers[i]}");
                    }
                }

                writer.ShowResults(results, test.GradeAvailability);
            }
        }

        private void PassTest(Test test, Result result, List<string> passAnswers)
        {
            test.Questions.ForEach(el =>
            {
                if (cancellToken.IsCancellationRequested)
                {
                    return;
                }

                var userAnswer = AnswerTheQuestion(test, el);

                if (test.ClosedQuestions)
                {
                    if (userAnswer == el.CorrectAnswer)
                    {
                        result.CorrectAnswers++;
                        result.Grade += el.QuestionRating;
                    }
                    else
                    {
                        result.IncorrectAnswers++;
                    }
                }
                else
                {
                    passAnswers.Add(userAnswer);
                }

                if (test.IndicateCorrectAnswer)
                {
                    Console.WriteLine(userAnswer == el.CorrectAnswer);
                }
            });
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
