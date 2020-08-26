using PBK.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PBK.Test_setup
{
    class TestCreator
    {
        internal static void CreateNewTest(string name)
        {
            Test newTest = new Test
            {
                TestName = name
            };

            CreateQuestions(newTest);

            JsonStreamer.Write(name, newTest); 
        }

        private static void CreateQuestions(Test test)
        {
            while (true)
            {
                if (int.TryParse(Writer.DataEntry("Enter number of questions:"), out int result))
                {
                    test.QuestionsNumber = result;

                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\aIncorrect input");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            while (true)
            {
                if (int.TryParse(Writer.DataEntry("Enter number of answers:"), out int result))
                {
                    test.AnswersNumber = result;

                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\aIncorrect input");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            InputQuestions(test);
        }

        private static void InputQuestions(Test test)
        {
            for(var i = 1; i <= test.QuestionsNumber; i++)
            {
                Question newQuestion = new Question
                {
                    QuestionText = Writer.DataEntry("Enter question's text:")
                };

                for (var j = 1; j <= test.AnswersNumber; j++)
                {
                    newQuestion.Answers.Add(Writer.DataEntry($"Enter answer #{j} :"));
                }

                foreach (string answer  in Writer.DataEntry("Enter correct answers:")
                    .Split(" ,.\t".ToCharArray()))
                {
                    newQuestion.CorrectAnswers.Add(answer);
                }
            }
        }
    }
}
