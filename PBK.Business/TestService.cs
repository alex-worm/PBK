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

        public void EditName(Test test, string newName)
        {
            var testList = _testsSerializer.Deserialize();

            testList.Find(el => el == test).Name = newName;

            _testsSerializer.Serialize(testList);
        }

        public void AddQuestion(Test test, Question newQuestion)
        {
            var testList = _testsSerializer.Deserialize();

            testList.Find(el => el == test).Questions.Add(newQuestion);

            _testsSerializer.Serialize(testList);
        }

        public void RemoveQuestion(Test test, int questionNumber)
        {
            var testList = _testsSerializer.Deserialize();
            
            if (questionNumber < 1 || questionNumber > test.QuestionsNumber)
            {
                throw new IndexOutOfRangeException(nameof(questionNumber));
            }

            testList.Find(el => el == test).Questions.Remove(test.Questions
                .Find(el => el.Number == questionNumber));
            
            _testsSerializer.Serialize(testList);
        }

        public void EditTimerValue(Test test, int newValue)
        {
            var testList = _testsSerializer.Deserialize();

            testList.Find(el => el == test).TimerValue = newValue;

            _testsSerializer.Serialize(testList);
        }

        public void Remove(Test test)
        {
            var testList = _testsSerializer.Deserialize();
            
            testList.Remove(test);

            _testsSerializer.Serialize(testList);
        }

        /*public string ExportResults(string name, string title, List<int> userAnswers)
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

        public string GetStats(Test test)
        {
            var testList = _testsSerializer.Deserialize();
            
            return testList.Find(el => el == test).ToString();
        }

        public Test GetTest(string name, string title)
        {
            var testList = _testsSerializer.Deserialize();

            var test = testList.Find(el => el.Name == name && el.Title == title);

            if (test == null)
            {
                throw new NullReferenceException(nameof(test));
            }

            return test;
        }
    }
}