using System;
using System.Collections.Generic;
using System.IO;
using Common;
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

            var index = testList.FindIndex(el =>
                el.Name == test.Name && el.Title == test.Title);

            test.Name = newName;
            
            testList[index] = test;

            _testsSerializer.Serialize(testList);
        }

        public void AddQuestion(Test test, Question newQuestion)
        {
            var testList = _testsSerializer.Deserialize();

            var index = testList.FindIndex(el =>
                el.Name == test.Name && el.Title == test.Title);

            test.Questions.Add(newQuestion);

            testList[index] = test;

            _testsSerializer.Serialize(testList);
        }

        public void RemoveQuestion(Test test, int questionNumber)
        {
            var testList = _testsSerializer.Deserialize();
            
            if (questionNumber < 1 || questionNumber > test.QuestionsNumber)
            {
                throw new IndexOutOfRangeException(nameof(questionNumber));
            }

            var index = testList.FindIndex(el =>
                el.Name == test.Name && el.Title == test.Title);

            test.Questions.Remove(
                test.Questions.Find(el => el.Number == questionNumber));

            testList[index] = test;
            
            _testsSerializer.Serialize(testList);
        }

        public void EditTimerValue(Test test, int newValue)
        {
            var testList = _testsSerializer.Deserialize();

            var index = testList.FindIndex(el =>
                el.Name == test.Name && el.Title == test.Title);

            test.TimerValue = newValue;

            testList[index] = test;

            _testsSerializer.Serialize(testList);
        }

        public void Remove(Test test)
        {
            var testList = _testsSerializer.Deserialize();

            testList.Remove(testList.Find(el =>
                el.Name == test.Name && el.Title == test.Title));

            _testsSerializer.Serialize(testList);
        }

        public string ExportResults(Test test, List<int> userAnswers, TimeSpan passTime)
        {
            var testList = _testsSerializer.Deserialize();

            var index = testList.FindIndex(el =>
                el.Name == test.Name && el.Title == test.Title);
            
            test.PassesNumber++;

            using (var fileWriter =
                new StreamWriter(
                    string.Concat(
                        TextForOutput.UserAnswersTxt, test.Name, test.PassesNumber)))
            {
                for (var i = 0; i < userAnswers.Count; i++)
                {
                    fileWriter.WriteLine(
                        string.Concat(
                            TextForOutput.UserAnswer, test.Questions[i].Text, userAnswers[i]));
                }
            }

            if (!test.IsClosedQuestions)
            {
                testList[index] = test;
                
                _testsSerializer.Serialize(testList);

                return string.Concat(TextForOutput.PassTime, passTime);
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

            testList[index] = test;
            
            _testsSerializer.Serialize(testList);

            return string.Concat(
                TextForOutput.FullResult, grade, correct, incorrect, passTime);
        }

        public string GetStats(Test test)
        {
            var testList = _testsSerializer.Deserialize();

            var index = testList.FindIndex(el =>
                el.Name == test.Name && el.Title == test.Title);
            
            return testList[index].ToString();
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