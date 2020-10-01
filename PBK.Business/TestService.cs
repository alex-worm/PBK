using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Common.Entities;
using Data;

namespace Logic
{
    public class TestService
    {
        private readonly TestsSerializer _testsSerializer;
        private readonly Stopwatch _stopwatch;

        public TestService()
        {
            _testsSerializer = new TestsSerializer();
            _stopwatch = new Stopwatch();
        }

        public void Add(Test test)
        {
            var testList = _testsSerializer.Deserialize();

            testList.Add(test);

            _testsSerializer.Serialize(testList);
        }

        public void EditName(string name, string title, string newName)
        {
            var testList = _testsSerializer.Deserialize();

            var test = testList.Find(el => el.Name == name && el.Title == title);

            if (test == null)
            {
                throw new NullReferenceException(nameof(test));
            }

            test.Name = newName;

            _testsSerializer.Serialize(testList);
        }

        public void AddQuestion(string name, string title, Question newQuestion)
        {
            var testList = _testsSerializer.Deserialize();

            var test = testList.Find(el => el.Name == name && el.Title == title);

            if (test == null)
            {
                throw new NullReferenceException(nameof(test));
            }
            
            test.Questions.Add(newQuestion);

            _testsSerializer.Serialize(testList);
        }

        public void RemoveQuestion(string name, string title, int questionNumber)
        {
            var testList = _testsSerializer.Deserialize();

            var test = testList.Find(el => el.Name == name && el.Title == title);

            if (test == null)
            {
                throw new NullReferenceException(nameof(test));
            }
            
            if (questionNumber < 1 || questionNumber > test.QuestionsNumber)
            {
                throw new IndexOutOfRangeException(nameof(questionNumber));
            }

            test.Questions.Remove(test.Questions
                .Find(el => el.Number == questionNumber));
            _testsSerializer.Serialize(testList);
        }

        public void EditTimerValue(string name, string title, int newValue)
        {
            var testList = _testsSerializer.Deserialize();

            var test = testList.Find(el => el.Name == name && el.Title == title);

            if (test == null)
            {
                throw new NullReferenceException(nameof(test));
            }
            
            test.TimerValue = newValue;

            _testsSerializer.Serialize(testList);
        }

        public void Remove(string name, string title)
        {
            var testList = _testsSerializer.Deserialize();

            var test = testList.Find(el => el.Name == name && el.Title == title);

            if (test == null)
            {
                throw new NullReferenceException(nameof(test));
            }
            
            testList.Remove(test);

            _testsSerializer.Serialize(testList);
        }

        /*public Test GetTest(string name, string title)
        {
            var testList = _testsSerializer.Deserialize();

            var test = testList.Find(el => el.Name == name && el.Title == title);

            if (test == null)
            {
                throw new NullReferenceException(nameof(test));
            }

            _stopwatch.Start();
            
            return test;
        }

        public string ExportResults(string name, string title, List<int> userAnswers)
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
        }*/

        public string GetStats(string name, string title)
        {
            var testList = _testsSerializer.Deserialize();

            var test = testList.Find(el => el.Name == name && el.Title == title);

            if (test == null)
            {
                throw new NullReferenceException(nameof(test));
            }
            
            return test.ToString();
        }
    }
}