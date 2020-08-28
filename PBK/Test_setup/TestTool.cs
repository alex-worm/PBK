using Microsoft.VisualBasic.FileIO;
using PBK.Logic;
using System;
using System.IO;

namespace PBK.Test_setup
{
    public class TestTool
    {
        public static void CreateNewTest(string name)
        {
            Test newTest = new Test
            {
                TestName = name
            };

            CreateQuestions(newTest);

            JsonStreamer.Write(newTest); 
        }

        public static string DeleteTest(string name)
        {
            try
            {
                File.Delete($"{name}.json");
                return "File deleted successfully";
            }
            catch
            {
                return "File not found";
                //при отсутствии файла catch не срабатывает, так что поищу способ чрез GetFiles
            }
        }

        public static void EditTest(string name)
        {
            Test test = JsonStreamer.Read(name);

            if (test == null)
            {
                Console.WriteLine("File not found or empty");

                return;
            }

            switch(Writer.DataEntry("Choose an option to change: "))
            {
                case "name":
                    test.TestName = Writer.DataEntry("Enter new file name: ");
                    DeleteTest(name);
                    JsonStreamer.Write(test);
                    return;

                default:
                    Writer.DataEntry("Incorrect input\nTo continue press Enter..");
                    return;
            }
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\aIncorrect input");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            while (true)
            {
                if (int.TryParse(Writer.DataEntry("Enter number of answers:"), out int result))
                {
                    test.AnswersNumber = result;

                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\aIncorrect input");
                Console.ForegroundColor = ConsoleColor.Gray;
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

                test.Questions.Add(newQuestion);
            }
        }
    }
}
