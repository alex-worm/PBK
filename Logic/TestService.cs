using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Data.Entities;

namespace Logic
{
    public class TestService
    {
        private readonly TestsSerializer _testsSerializer;
        private readonly List<Test> _testList;
        private readonly Stopwatch _stopwatch;
        private readonly CancellationTokenSource _cancelToken;

        private const int MlSecsInMinute = 60000;

        private Test _test;

        public TestService(string name, string title)
        {
            _testsSerializer = new TestsSerializer();
            _testList = _testsSerializer.Deserialize();
            _test = _testList.Find(el => el.Name == name
                                         && el.Title == title);


            if (_test == null)
            {
                throw new NullReferenceException(nameof(_test));
            }

            _stopwatch = new Stopwatch();
            _cancelToken = new CancellationTokenSource();
        }

        public void Add(string name, string title, int questionsNumber,
            bool isClosedQuestions, bool isGradeAvailable, int timerValue)
        {
            _test = new Test
            {
                Name = name,
                Title = title,
                QuestionsNumber = questionsNumber,
                IsClosedQuestions = isClosedQuestions,
                IsScoreShown = isGradeAvailable,
                TimerValue = timerValue
            };

            var testList = _testsSerializer.Deserialize();

            testList.Add(_test);

            _testsSerializer.Serialize(testList);
        }

        public void EditName(string newName)
        {
            var testList = _testsSerializer.Deserialize();

            _test.Name = newName;

            _testsSerializer.Serialize(testList);
        }

        public void AddQuestion(string text, List<string> answers,
            int correctAnswer, int score)
        {
            _test.Questions.Add(GetQuestion(text, _test.QuestionsNumber + 1,
                answers, correctAnswer, score));

            _testsSerializer.Serialize(_testList);
        }

        private Question GetQuestion(string text, int questionNumber,
            List<string> answers, int correctAnswer, int score)
        {
            var newQuestion = new Question
            {
                Text = text,
                Number = questionNumber,
                Answers = answers,
                CorrectAnswer = correctAnswer
            };

            if (score != 0)
            {
                newQuestion.Score = score;
            }

            return newQuestion;
        }

        public void RemoveQuestion(int questionNumber)
        {
            if (questionNumber < 1 || questionNumber > _test.QuestionsNumber)
            {
                throw new IndexOutOfRangeException(nameof(questionNumber));
            }

            _test.Questions.Remove(_test.Questions
                .Find(el => el.Number == questionNumber));
            _testsSerializer.Serialize(_testList);
        }

        public void EditTimerValue(int newValue)
        {
            _test.TimerValue = newValue;

            _testsSerializer.Serialize(_testList);
        }

        public void Remove()
        {
            _testList.Remove(_test);

            _testsSerializer.Serialize(_testList);
        }

        public (string, List<string>) GetQuestion(int questionNumber)
        {
            if (!_stopwatch.IsRunning)
            {
                _stopwatch.Start();
                _cancelToken.CancelAfter(_test.TimerValue * MlSecsInMinute);
            }

            if (_cancelToken.IsCancellationRequested && _test.TimerValue != 0)
            {
                return (null, null);
            }

            return (_test.Questions[questionNumber - 1].Text,
                _test.Questions[questionNumber - 1].Answers);
        }

        public string ExportResults(List<int> userAnswers)
        {
            _stopwatch.Stop();

            _test.PassesNumber++;

            using (var fileWriter =
                new StreamWriter($@"{_test.Name}({_test.PassesNumber}).txt"))
            {
                for (var i = 0; i < userAnswers.Count; i++)
                {
                    fileWriter.WriteLine(
                        $"{_test.Questions[i].Text}: {userAnswers[i]}");
                }
            }

            if (!_test.IsClosedQuestions)
            {
                return null;
            }

            var grade = 0;
            var correct = 0;
            var incorrect = 0;

            for (var i = 0; i < userAnswers.Count; i++)
            {
                if (userAnswers[i] == _test.Questions[i].CorrectAnswer)
                {
                    correct++;
                    grade += _test.Questions[i].Score;
                }
                else
                {
                    incorrect++;
                }
            }

            _test.TotalCorrectAnswers += correct;
            _test.TotalIncorrectAnswers += incorrect;

            return string.Concat("Grade: {0} Correct: {1} Incorrect: {2}", grade,
                correct, incorrect);
        }

        public string GetStats()
        {
            return _test.ToString();
        }
    }
}