using PBK.Entities;
using PBK.Logic.TestEditing;
using PBK.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using PBK.Logic.TopicEditing;

namespace PBK.Logic.TestPassing
{
    public class Tester
    {
        private delegate void ResultHandler(Result result);
        private event ResultHandler PassEnded;

        private const int mlSecsInMinute = 60000;

        private readonly CancellationTokenSource _cancelToken = new CancellationTokenSource();

        public void PassTest(string name)
        {
            var testSerializer = new TestSerializer();
            var writer = new ConsoleOutput();
            var result = new Result();

            var test = testSerializer.Deserialize(name);

            if (test == null)
            {
                writer.PrintMessage(TextForOutput.NotOpened);
                return;
            }

            if (test.TimerValue != 0)
            {
                _cancelToken.CancelAfter(test.TimerValue * mlSecsInMinute);
            }

            GetPassResults(test, result);

            OutputResults(test, result);
        }

        private void GetPassResults(Test test, Result result)
        {
            var writer = new ConsoleOutput();
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            test.Questions.ForEach(el =>
            {
                if (_cancelToken.IsCancellationRequested)
                {
                    return;
                }

                var userAnswer = AnswerTheQuestion(el);

                if (userAnswer == el.CorrectAnswer)
                {
                    result.CorrectAnswers++;
                    result.Grade += el.QuestionRating;
                }
                else
                {
                    result.IncorrectAnswers++;
                }

                result.UserAnswers.Add(userAnswer);

                if (test.IsIndicateAnswer)
                {
                    writer.PrintMessage((userAnswer == el.CorrectAnswer).ToString());
                }
            });

            stopwatch.Stop();
            result.PassTime = stopwatch.Elapsed;
        }

        private string AnswerTheQuestion(Question question)
        {
            var writer = new ConsoleOutput();

            writer.PrintMessage(question.QuestionText);

            for (var i = 0; i < question.AnswersNumber; i++)
            {
                writer.PrintMessage($"{i + 1}. {question.Answers[i]}");
            }

            return Console.ReadLine();
        }

        private void OutputResults(Test test, Result result)
        {
            var writer = new ConsoleOutput();
            var testSerializer = new TestSerializer();
            var topicSerializer = new TopicSerializer();

            test.PassesNumber++;
            test.TotalCorrectAnswers += result.CorrectAnswers;
            test.TotalIncorrectAnswers += result.IncorrectAnswers;

            using (var fileWriter = new StreamWriter($@"{test.Name}({test.PassesNumber}).txt"))
            {
                for (var i = 0; i < result.CorrectAnswers + result.IncorrectAnswers; i++)
                {
                    fileWriter.WriteLine($"{test.Questions[i].QuestionText}: {result.UserAnswers[i]}");
                }
            }

            PassEnded = writer.PrintTimeResult;

            if (test.IsClosedQuestions)
            {
                PassEnded += writer.PrintAnswersCorrectness;
            }

            if (test.IsGradeAvailable)
            {
                PassEnded += writer.PrintGrade;
            }

            PassEnded?.Invoke(result);

            testSerializer.Serialize(test);

            var topic = topicSerializer.Deserialize(test.TopicName);

            topic.IncludedTests.Find(el => el.Name == test.Name).PassesNumber = test.PassesNumber;
            topic.IncludedTests.Find(el => el.Name == test.Name).TotalCorrectAnswers = test.TotalCorrectAnswers;
            topic.IncludedTests.Find(el => el.Name == test.Name).TotalIncorrectAnswers = test.TotalIncorrectAnswers;

            topicSerializer.Serialize(topic);
        }
    }
}
