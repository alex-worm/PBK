using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Entities;
using Common.Enums;
using Logic;

namespace UI
{
    public static class Starter
    {
        private static readonly ConsoleHandler Console;
        private static readonly TestService TestService;
        private static readonly CancellationTokenSource CancelToken;

        private const int MlSecsInMinute = 60000;

        static Starter()
        {
            Console = new ConsoleHandler();
            TestService = new TestService();
            CancelToken = new CancellationTokenSource();
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
                        //PassTest();
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
            var test = new Test
            {
                Name = Console.GetInput(TextForOutput.EnterNameToAdd),
                Title = Console.GetInput(TextForOutput.EnterTopic),
                QuestionsNumber = GetIntValue(TextForOutput.EnterQuestionsNumber),
                IsClosedQuestions = GetBoolValue(TextForOutput.ChooseQuestionsType),
                IsIndicateAnswers =
                    GetBoolValue(TextForOutput.EnableIndicateCorrectAnswer)
            };
            
            test.IsScoreShown = test.IsClosedQuestions &&
                                GetBoolValue(TextForOutput.EnableShowGrade);

            for (var i = 1; i <= test.QuestionsNumber; i++)
            {
                AddQuestion(test.Name, test.Title);
            }

            test.TimerValue = GetIntValue(TextForOutput.EnterTimerValue);

            TestService.Add(test);
        }

        private static void EditTest()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            var numberOfValue = GetIntValue(TextForOutput.ChooseTestValueToChange);

            switch (numberOfValue)
            {
                case (int) ValueToEditTest.Name:
                    var newName = Console.GetInput(TextForOutput.EnterNewName);
                    TestService.EditName(name, title, newName);
                    break;

                case (int) ValueToEditTest.AddQuestion:
                    AddQuestion(name, title);
                    break;

                case (int) ValueToEditTest.RemoveQuestion:
                    var questionNumber = GetIntValue(TextForOutput.EnterQuestionNumber);
                    TestService.RemoveQuestion(name, title, questionNumber);
                    break;

                case (int) ValueToEditTest.TimerValue:
                    var timerValue = GetIntValue(TextForOutput.EnterTimerValue);
                    TestService.EditTimerValue(name, title, timerValue);
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

            TestService.Remove(name, title);
        }

        /*private static void PassTest()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);
            
            var test = TestService.GetTest(name, title);

            var userAnswers = new List<int>();

            if (test.TimerValue != 0)
            {
                CancelToken.CancelAfter(test.TimerValue * MlSecsInMinute);
            }

            foreach (var question in test.Questions
                .TakeWhile(question => !CancelToken.IsCancellationRequested))
            {
                Console.ShowMessage(question.Text);

                question.Answers.ForEach(answer => Console.ShowMessage(question.Text));

                var userAnswer = GetIntValue(TextForOutput.EnterAnswer);

                while (userAnswer < 1 || userAnswer > question.Answers.Count)
                {
                    Console.ShowMessage(TextForOutput.IncorrectInput);

                    userAnswer = GetIntValue(TextForOutput.EnterAnswer);
                }

                if (test.IsIndicateAnswers)
                {
                    Console.ShowMessage((userAnswer == question.CorrectAnswer)
                        .ToString());
                }

                userAnswers.Add(userAnswer);
            }

            var results = TestService.ExportResults(test userAnswers);

            if (results != null)
            {
                Console.ShowMessage(results);
            }
        }*/

        private static void ShowStats()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            Console.ShowMessage(TestService.GetStats(name, title));
        }

        private static void AddQuestion(string name, string title)
        {
            var newQuestion = new Question
            {
                Text = Console.GetInput(TextForOutput.EnterQuestionText)
            };

            var answersNumber = GetIntValue(TextForOutput.EnterAnswersNumber);

            for (var j = 0; j < answersNumber; j++)
            {
                newQuestion.Answers.Add(Console.GetInput(TextForOutput.EnterAnswer));
            }

            newQuestion.CorrectAnswer = GetIntValue(TextForOutput.EnterCorrectAnswer);

            newQuestion.Score = GetIntValue(TextForOutput.EnterQuestionScore);

            TestService.AddQuestion(name, title, newQuestion);
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