using PBK.Entities;
using PBK.Logic.EntityEditing;
using PBK.UI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PBK.Logic.TestPassing
{
    class Tester
    {
        private static Test _test;

        public async static void Countdown(Test test)
        {
            await Task.Run(() => Writer.ShowResult(test));
        }

        public static void PassTest(string name)
        {
            _test = TestTool.Read(name);
            if (_test == null)
            {
                Console.WriteLine(TextForOutput.notOpened);
                return;
            }

            var userCorrect = 0;
            var userGrade = 0;

            if (_test.TimerValue != 0)
            {
                Countdown(_test);
            }
            
            foreach (var question in _test.Questions)
            {
                Console.WriteLine(question.QuestionText);
                if (GetAnswer(question))
                {
                    userCorrect++;
                    if (_test.ClosedQuestions)
                    {
                        userGrade += question.QuestionRating;
                    }
                }
            }
        }

        private static bool GetAnswer(Question question)
        {
            for(var i = 0; i < question.AnswersNumber; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Answers[i]}");
            }

            var userAnswer = Console.ReadLine();
            if (_test.IndicateCorrectAnswer)
            {
                Console.WriteLine(userAnswer == question.CorrectAnswer);
            }

            return userAnswer == question.CorrectAnswer;
        }
    }
}
