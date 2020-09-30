using System.Collections.Generic;
using Logic;
using UI.Enums;

namespace UI
{
    public static class Starter
    {
        private static readonly ConsoleHandler Console;

        static Starter()
        {
            Console = new ConsoleHandler();
        }

        public static void Start()
        {
            Console.ShowMessage(TextForOutput.Greeting);

            while (true)
            {
                var action = GetIntValue(TextForOutput.EnterStarterCommand);

                switch (action)
                {
                    case (int) FirstAction.AddTest:
                        AddTest();
                        break;

                    case (int) FirstAction.EditTest:
                        EditTest();
                        break;

                    case (int) FirstAction.RemoveTest:
                        RemoveTest();
                        break;

                    case (int) FirstAction.PassTest:
                        PassTest();
                        break;

                    case (int) FirstAction.ShowStats:
                        ShowStats();
                        break;

                    case (int) FirstAction.Exit:
                        return;
                }
            }
        }

        private static void AddTest()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToAdd);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            var testService = new TestService(name, title);

            var questionsNumber = GetIntValue(TextForOutput.EnterQuestionsNumber);

            var isClosedQuestions = GetBoolValue(TextForOutput.ChooseQuestionsType);

            var isGradeAvailable = isClosedQuestions &&
                                   GetBoolValue(TextForOutput.EnableShowGrade);

            for (var i = 1; i <= questionsNumber; i++)
            {
                AddQuestion(testService);
            }

            var timerValue = GetIntValue(TextForOutput.EnterTimerValue);

            testService.Add(name, title, questionsNumber, isClosedQuestions,
                isGradeAvailable,
                timerValue);
        }

        private static void EditTest()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            var testService = new TestService(name, title);

            var numberOfValue = GetIntValue(TextForOutput.ChooseTestValueToChange);

            switch (numberOfValue)
            {
                case (int) ValueToEditTest.Name:
                    var newName = Console.GetInput(TextForOutput.EnterNewName);
                    testService.EditName(newName);
                    break;

                case (int) ValueToEditTest.AddQuestion:
                    AddQuestion(testService);
                    break;

                case (int) ValueToEditTest.RemoveQuestion:
                    var questionNumber = GetIntValue(TextForOutput.EnterQuestionNumber);
                    testService.RemoveQuestion(questionNumber);
                    break;

                case (int) ValueToEditTest.TimerValue:
                    var timerValue = GetIntValue(TextForOutput.EnterTimerValue);
                    testService.EditTimerValue(timerValue);
                    break;

                default:
                    Console.ShowMessage(TextForOutput.IncorrectInput);
                    break;
            }
        }

        private static void RemoveTest()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            var testService = new TestService(name, title);

            testService.Remove();
        }

        private static void PassTest()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            var testService = new TestService(name, title);

            var userAnswers = new List<int>();

            for (var i = 1;; i++)
            {
                var (item1, item2) = testService.GetQuestion(i);

                if (item1 == null)
                {
                    break;
                }

                Console.ShowMessage(item1);

                for (var j = 0; j < item2.Count; j++)
                {
                    Console.ShowMessage(
                        string.Format(TextForOutput.AnswerNumber, j, item2[j]));
                }

                int userAnswer;
                do
                {
                    userAnswer = GetIntValue(TextForOutput.EnterAnswer);
                } while (userAnswer < 1 || userAnswer > item2.Count);

                userAnswers.Add(userAnswer);
            }

            var results = testService.ExportResults(userAnswers);

            if (results != null)
            {
                Console.ShowMessage(results);
            }
        }

        private static void ShowStats()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            var testService = new TestService(name, title);

            Console.ShowMessage(testService.GetStats());
        }

        private static void AddQuestion(TestService testService)
        {
            var text = Console.GetInput(TextForOutput.EnterQuestionText);

            var answersNumber = GetIntValue(TextForOutput.EnterAnswersNumber);

            var answers = new List<string>();
            for (var j = 0; j < answersNumber; j++)
            {
                answers.Add(Console.GetInput(TextForOutput.EnterAnswer));
            }

            var correctAnswer = GetIntValue(TextForOutput.EnterCorrectAnswer);

            var score = GetIntValue(TextForOutput.EnterQuestionScore);

            testService.AddQuestion(text, answers, correctAnswer,
                score);
        }

        private static int GetIntValue(string message)
        {
            int result;

            while (!int.TryParse(Console.GetInput(message), out result))
            {
                Console.ShowMessage(TextForOutput.IncorrectInput);
            }

            return result;
        }

        private static bool GetBoolValue(string message)
        {
            int result;

            while (!int.TryParse(Console.GetInput(message),
                out result) && result != 1 && result != 2)
            {
                Console.ShowMessage(TextForOutput.IncorrectInput);
            }

            return result == 1;
        }
    }
}