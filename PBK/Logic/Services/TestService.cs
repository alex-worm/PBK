using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using PBK.Entities;
using PBK.Enums;
using PBK.Logic.Serializers;
using PBK.UI;

namespace PBK.Logic.Services
{
    public class TestService
    {
        private delegate void ResultHandler(Result result);
        private event ResultHandler PassEnded;
        
        private readonly ConsoleHandler _console;
        private readonly QuestionService _questionService;
        private readonly Result _result;
        private readonly TestSerializer _testSerializer;
        private readonly TopicSerializer _topicSerializer;
        private readonly Stopwatch _stopwatch;
        private readonly CancellationTokenSource _cancelToken;

        private const int mlSecsInMinute = 60000;

        public TestService()
        {
            _console = new ConsoleHandler();
            _questionService = new QuestionService();
            _result = new Result();
            _testSerializer = new TestSerializer();
            _topicSerializer = new TopicSerializer();
            _stopwatch = new Stopwatch();
            _cancelToken = new CancellationTokenSource();
        }

        public void DeleteTest(string name)
        {
            var test = _testSerializer.Deserialize(name);

            if (test == null)
            {
                _console.ShowMessage(TextForOutput.NotOpened);

                return;
            }

            var topic = _topicSerializer.Deserialize(test.TopicName);

            topic.IncludedTests.Remove(topic.IncludedTests.Find(el => el.Name == test.Name));
            _topicSerializer.Serialize(topic);

            File.Delete($"{test.Name}.json");

            _console.ShowMessage(TextForOutput.FileDeleted);
        }

        public void EditTest(string name)
        {
            var test = _testSerializer.Deserialize(name);

            if (test == null)
            {
                _console.ShowMessage(TextForOutput.IncorrectInput);
                return;
            }

            if (!int.TryParse(_console.GetInput(TextForOutput.ChooseTestValueToChange), out var parseResult))
            {
                _console.ShowMessage(TextForOutput.IncorrectInput);
                return;
            }

            switch (parseResult)
            {
                case (int)ValueToEditTest.Name:
                    test.Name = _console.GetInput(TextForOutput.EnterNewName);
                    DeleteTest(name);
                    _testSerializer.Serialize(test);
                    break;

                case (int)ValueToEditTest.AddQuestion:
                    test.QuestionsNumber++;
                    _questionService.GetQuestion(test, test.QuestionsNumber);
                    break;

                case (int)ValueToEditTest.EditQuestion:
                    while (!int.TryParse(_console.GetInput(TextForOutput.EnterQuestionNumber),
                               out parseResult) && parseResult < 1)
                    {
                        _console.ShowMessage(TextForOutput.IncorrectInput);
                    }
                    test.Questions[parseResult - 1] = _questionService.GetQuestion(test, parseResult);
                    break;

                case (int)ValueToEditTest.TimerValue:
                    test.TimerValue = GetIntValue(TextForOutput.EnterTimerValue);
                    break;

                default:
                    _console.ShowMessage(TextForOutput.IncorrectInput);
                    return;
            }

            _testSerializer.Serialize(test);
        }

        public void CreateTest(string name)
        {
            var test = new Test
            {
                Name = name
            };

            var testTopic = SetTopic(test);
            testTopic.IncludedTests.Add(test);

            test.IsClosedQuestions = GetBoolValue(TextForOutput.ChooseQuestionsType);
            test.QuestionsNumber = GetIntValue(TextForOutput.EnterQuestionsNumber);

            if (test.IsClosedQuestions)
            {
                test.IsIndicateAnswer = GetBoolValue(TextForOutput.EnableIndicateCorrectAnswer);
                test.IsGradeAvailable = GetBoolValue(TextForOutput.EnableShowGrade);
            }

            for (var i = 1; i <= test.QuestionsNumber; i++)
            {
                test.Questions.Add(_questionService.GetQuestion(test, i));
            }

            test.TimerValue = GetIntValue(TextForOutput.EnterTimerValue);

            _testSerializer.Serialize(test);
            _topicSerializer.Serialize(testTopic);
        }

        private Topic SetTopic(BriefTestInfo test)
        {
            var topicName = _console.GetInput(TextForOutput.EnterTopic);
            test.TopicName = topicName;

            var topic = _topicSerializer.Deserialize(topicName);

            if (topic != null)
            {
                return topic;
            }

            topic = new Topic()
            {
                Title = topicName
            };

            return topic;
        }

        private int GetIntValue(string message)
        {
            int result;
            
            while (!int.TryParse(_console.GetInput(message), out result))
            {
                _console.ShowMessage(TextForOutput.IncorrectInput);
            }

            return result;
        }
        
        private bool GetBoolValue(string message)
        {
            int result;

            while (!int.TryParse(_console.GetInput(message),
                       out result) && (result != 1 || result != 2))
            {
                _console.ShowMessage(TextForOutput.IncorrectInput);
            }

            return result == 1;
        }

        public void PassTest(string name)
        {
            var test = _testSerializer.Deserialize(name);

            if (test == null)
            {
                _console.ShowMessage(TextForOutput.NotOpened);
                return;
            }

            if (test.TimerValue != 0)
            {
                _cancelToken.CancelAfter(test.TimerValue * mlSecsInMinute);
            }

            GetPassResults(test, _result);

            ExportResults(test, _result);
        }

        private void GetPassResults(Test test, Result result)
        {
            _stopwatch.Start();

            test.Questions.ForEach(el =>
            {
                if (_cancelToken.IsCancellationRequested)
                {
                    return;
                }

                var userAnswer = AnswerTheQuestion(el);
                result.UserAnswers.Add(userAnswer);

                if (userAnswer == el.CorrectAnswer)
                {
                    result.CorrectAnswers++;
                    result.Grade += el.Score;
                }
                else
                {
                    result.IncorrectAnswers++;
                }

                if (test.IsIndicateAnswer)
                {
                    _console.ShowMessage((userAnswer == el.CorrectAnswer).ToString());
                }
            });

            _stopwatch.Stop();
            result.PassTime = _stopwatch.Elapsed;
        }

        private string AnswerTheQuestion(Question question)
        {
            _console.ShowMessage(question.Text);

            for (var i = 0; i < question.Answers.Count; i++)
            {
                _console.ShowMessage($"{i + 1}. {question.Answers[i]}");
            }

            return Console.ReadLine();
        }

        private void ExportResults(Test test, Result result)
        {
            test.PassesNumber++;
            test.TotalCorrectAnswers += result.CorrectAnswers;
            test.TotalIncorrectAnswers += result.IncorrectAnswers;

            using (var fileWriter = new StreamWriter($@"{test.Name}({test.PassesNumber}).txt"))
            {
                for (var i = 0; i < result.CorrectAnswers + result.IncorrectAnswers; i++)
                {
                    fileWriter.WriteLine($"{test.Questions[i].Text}: {result.UserAnswers[i]}");
                }
            }

            PassEnded = _console.ShowPassTime;

            if (test.IsClosedQuestions)
            {
                PassEnded += _console.ShowAnswersCorrectness;
            }

            if (test.IsGradeAvailable)
            {
                PassEnded += _console.ShowGrade;
            }

            PassEnded?.Invoke(result);

            _testSerializer.Serialize(test);

            var topic = _topicSerializer.Deserialize(test.TopicName);

            topic.IncludedTests.Find(el => el.Name == test.Name).PassesNumber = test.PassesNumber;
            topic.IncludedTests.Find(el => el.Name == test.Name).TotalCorrectAnswers = test.TotalCorrectAnswers;
            topic.IncludedTests.Find(el => el.Name == test.Name).TotalIncorrectAnswers = test.TotalIncorrectAnswers;

            _topicSerializer.Serialize(topic);
        }
    }
}