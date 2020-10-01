using System.Collections.Generic;
using System.Diagnostics;
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
        private static CancellationTokenSource CancelToken;
        private static Stopwatch Stopwatch;

        private const int MlSecsInMinute = 60000;

        static Starter()
        {
            Console = new ConsoleHandler();
            TestService = new TestService();
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
            var test = new Test
            {
                Name = Console.GetInput(TextForOutput.EnterNameToAdd),
                Title = Console.GetInput(TextForOutput.EnterTopic),
                QuestionsNumber = GetIntValue(TextForOutput.EnterQuestionsNumber),
                IsClosedQuestions = GetBoolValue(TextForOutput.ChooseQuestionsType)
            };

            if (test.IsClosedQuestions)
            {
                test.IsIndicateAnswers =
                    GetBoolValue(TextForOutput.EnableIndicateCorrectAnswer);
                
                test.IsScoreShown = GetBoolValue(TextForOutput.EnableShowGrade);
            }

            for (var i = 1; i <= test.QuestionsNumber; i++)
            {
                AddQuestion(test);
            }

            test.TimerValue = GetIntValue(TextForOutput.EnterTimerValue);

            TestService.Add(test);
        }

        private static void EditTest()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            var test = TestService.GetTest(name, title);

            var numberOfValue = GetIntValue(TextForOutput.ChooseTestValueToChange);

            switch (numberOfValue)
            {
                case (int) ValueToEditTest.Name:
                    var newName = Console.GetInput(TextForOutput.EnterNewName);
                    TestService.EditName(test, newName);
                    break;

                case (int) ValueToEditTest.AddQuestion:
                    AddQuestion(test);
                    break;

                case (int) ValueToEditTest.RemoveQuestion:
                    var questionNumber = GetIntValue(TextForOutput.EnterQuestionNumber);
                    TestService.RemoveQuestion(test, questionNumber);
                    break;

                case (int) ValueToEditTest.TimerValue:
                    var timerValue = GetIntValue(TextForOutput.EnterTimerValue);
                    TestService.EditTimerValue(test, timerValue);
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

            var test = TestService.GetTest(name, title);

            TestService.Remove(test);
        }

        private static void PassTest()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);
            
            var test = TestService.GetTest(name, title);

            var userAnswers = new List<int>();

            CancelToken = new CancellationTokenSource();
            Stopwatch = new Stopwatch();
            
            Stopwatch.Start();
            
            if (test.TimerValue != 0)
            {
                CancelToken.CancelAfter(test.TimerValue * MlSecsInMinute);
            }

            foreach (var question in test.Questions)
            {
                if (CancelToken.IsCancellationRequested)
                {
                    Console.ShowMessage(TextForOutput.PassIsEnded);
                    break;
                }
                
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
            
            Stopwatch.Stop();

            var results = TestService.ExportResults(test, userAnswers,
                Stopwatch.Elapsed);

            Console.ShowMessage(results);
        }

        private static void ShowStats()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            var test = TestService.GetTest(name, title);

            Console.ShowMessage(TestService.GetStats(test));
        }

        private static void AddQuestion(Test test)
        {
            var newQuestion = new Question
            {
                Text = Console.GetInput(TextForOutput.EnterQuestionText),
                Number = ++test.QuestionsNumber
            };

            if (test.IsClosedQuestions)
            {
                var answersNumber = GetIntValue(TextForOutput.EnterAnswersNumber);

                for (var j = 0; j < answersNumber; j++)
                {
                    newQuestion.Answers.Add(Console.GetInput(TextForOutput.EnterAnswer));
                }

                newQuestion.CorrectAnswer = GetIntValue(TextForOutput.EnterCorrectAnswer);

                newQuestion.Score = GetIntValue(TextForOutput.EnterQuestionScore);
            }
            
            test.Questions.Add(newQuestion);

            TestService.AddQuestion(test);
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