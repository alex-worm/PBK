using System;
using System.Collections.Generic;
using System.IO;
using Common.Entities;
using Data;

namespace Logic
{
    public class TestService
    {
        private readonly TestsSerializer _testsSerializer;

        public TestService()
        {
            _testsSerializer = new TestsSerializer();
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

            testList.Find(el => el.Name == test.Name && el.Title == test.Title)
                .Name = newName;

            _testsSerializer.Serialize(testList);
        }

        public void AddQuestion(Test test)
        {
            var testList = _testsSerializer.Deserialize();

            testList.Remove(testList.Find(el =>
                el.Name == test.Name && el.Title == test.Title));

            testList.Add(test);

            _testsSerializer.Serialize(testList);
        }

        public void RemoveQuestion(Test test, int questionNumber)
        {
            var testList = _testsSerializer.Deserialize();
            
            if (questionNumber < 1 || questionNumber > test.QuestionsNumber)
            {
                throw new IndexOutOfRangeException(nameof(questionNumber));
            }

            testList.Find(el => el.Name == test.Name && el.Title == test.Title)
                .Questions.Remove(test.Questions.Find(el => el.Number == questionNumber));
            
            _testsSerializer.Serialize(testList);
        }

        public void EditTimerValue(Test test, int newValue)
        {
            var testList = _testsSerializer.Deserialize();

            testList.Find(el => el.Name == test.Name && el.Title == test.Title)
                    .TimerValue = newValue;

            _testsSerializer.Serialize(testList);
        }

        public void Remove(Test test)
        {
            var testList = _testsSerializer.Deserialize();
            
            testList.Remove(test);

            _testsSerializer.Serialize(testList);
        }

        public string ExportResults(Test test, List<int> userAnswers, TimeSpan passTime)
        {
            var testList = _testsSerializer.Deserialize();
            
            test.PassesNumber++;

            using (var fileWriter =
                new StreamWriter($@"{test.Name}({test.PassesNumber}).txt"))
            {
                for (var i = 0; i < userAnswers.Count; i++)
                {
                    fileWriter.WriteLine(
                        $"{test.Questions[i].Text}: {userAnswers[i]}");
                }
            }

            if (!test.IsClosedQuestions)
            {
                testList.Find(el => el.Name == test.Name && el.Title == test.Title)
                    .PassesNumber++;
                
                _testsSerializer.Serialize(testList);
                
                return string.Concat("Pass time: {0}", passTime);
            }

            var grade = 0;
            var correct = 0;
            var incorrect = 0;

            for (var i = 0; i < userAnswers.Count; i++)
            {
                if (userAnswers[i] == test.Questions[i].CorrectAnswer)
                {
                    correct++;
                    grade += test.Questions[i].Score;
                }
                else
                {
                    incorrect++;
                }
            }

            test.TotalCorrectAnswers += correct;
            test.TotalIncorrectAnswers += incorrect;

            testList.Find(el => el.Name == test.Name && el.Title == test.Title)
                .PassesNumber++;
            testList.Find(el => el.Name == test.Name && el.Title == test.Title)
                .TotalCorrectAnswers += correct;
            testList.Find(el => el.Name == test.Name && el.Title == test.Title)
                .TotalIncorrectAnswers += incorrect;
            
            _testsSerializer.Serialize(testList);

            return string.Concat("\nGrade: {0}\nCorrect: {1}\nIncorrect: {2}\nPass time: {3}\n\n", grade,
                correct, incorrect);
        }

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