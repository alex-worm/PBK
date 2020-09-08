using PBK.UI;
using PBK.Logic;
using System;
using System.IO;

namespace PBK.Test_setup
{
    public class TestTool
    {
        public static void DeleteTest(string name)
        {
            string fileName = $"{name}.json";

            if (!File.Exists(fileName))
            {
                Writer.DataEntry(TextForOutput.fileNotOpened);
            }

            File.Delete(fileName);

            Writer.DataEntry(TextForOutput.fileDeleted);
        }

        public static void OpenTest(string name)
        {
            Test test = JsonStreamer.Read(name);

            if (test == null)
            {
                Writer.DataEntry(TextForOutput.fileNotOpened);

                return;
            }
            
        }

        public static void EditTest(string name)
        {
            Test test = JsonStreamer.Read(name);

            if (test == null)
            {
                Writer.DataEntry(TextForOutput.fileNotOpened);

                return;
            }

            if(!int.TryParse(Writer.DataEntry("Choose a value to change:\n1. Name\n2. Number of questions\n3. Number of answers\n4. Question"), out int result))
            {
                Writer.DataEntry("Incorrect input\nTo continue press Enter..");

                return;
            }

            switch (result)
            {
                case (int)TestValueToEdit.Name:
                    test.TestName = Writer.DataEntry("Enter new file's name: ");
                    DeleteTest(name);
                    JsonStreamer.Write(test);
                    return;
            }
        }

        public static void CreateNewTest(string name)
        {
            Test newTest = new Test
            {
                TestName = name
            };
            newTest.TestTopic.Title = Writer.DataEntry(TextForOutput.inputTopic);

            CreateQuestions(newTest);

            JsonStreamer.Write(newTest);
        }

        private static void CreateQuestions(Test test)
        {
            if (!int.TryParse(Writer.DataEntry(TextForOutput.questionsNumber), out int result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                CreateQuestions(test);
            }
            test.QuestionsNumber = result;

            if (Writer.DataEntry(TextForOutput.closedQuestions) == "1")
            {
                test.ClosedQuestions = true;
                test.AnswersNumber = 0;
                test.IndicateCorrectAnswer = false;
                test.TotalGradeAvailability = false;
            }
            else test.ClosedQuestions = false;

            test.IndicateCorrectAnswer = Writer.DataEntry(TextForOutput.indicateAnswers) == "1";
            test.TotalGradeAvailability = Writer.DataEntry(TextForOutput.totalGrade) == "1";

            bool correctInput;
            do
            {
                correctInput = !int.TryParse(Writer.DataEntry(TextForOutput.timerValue), out result);
            }
            while (correctInput);
            test.TimerValue = result;

            do
            {
                correctInput = !int.TryParse(Writer.DataEntry(TextForOutput.answersNumber), out result);
            }
            while (correctInput);
            test.AnswersNumber = result;

            for(var i = 1; i <= test.QuestionsNumber; i++)
            {
                InputQuestions(test, i);
            }
        }

        private static void InputQuestions(Test test, int i)
        {
            Question newQuestion = new Question
            {
                QuestionText = Writer.DataEntry(TextForOutput.questionText),
                QuestionNumber = i
            };

            for (var j = 1; j <= test.AnswersNumber; j++)
            {
                newQuestion.Answers.Add(Writer.DataEntry(TextForOutput.enterAnswer));
            }

            foreach (var answer in Writer.DataEntry(TextForOutput.correctAnswers)
                .Split(" ,.\t".ToCharArray()))
            {
                newQuestion.CorrectAnswers.Add(answer);
            }

            bool correctInput;
            int points;
            do
            {
                correctInput = !int.TryParse(Writer.DataEntry(TextForOutput.answersNumber), out points);
            }
            while (correctInput);
            newQuestion.QuestionRating = points;

            test.Questions.Add(newQuestion);
        }
    }
}
