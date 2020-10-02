using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Common;
using Common.Entities;
using Common.Enums;
using Logic;

namespace UI
{
    public static class Starter
    {
        private static readonly ConsoleHandler Console;
        private static readonly TestService TestService;
        private static CancellationTokenSource _cancelToken;
        private static Stopwatch _stopwatch;

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
            var name = Console.GetInput(TextForOutput.EnterNameToRemove);
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

            _cancelToken = new CancellationTokenSource();
            _stopwatch = new Stopwatch();
            
            _stopwatch.Start();
            
            if (test.TimerValue != 0)
            {
                _cancelToken.CancelAfter(test.TimerValue * MlSecsInMinute);
            }

            foreach (var question in test.Questions)
            {
                if (_cancelToken.IsCancellationRequested)
                {
                    Console.ShowMessage(TextForOutput.PassIsEnded);
                    break;
                }
                
                Console.ShowMessage(question.Text);

                for (var i = 0; i < question.Answers.Count; i++)
                {
                    Console.ShowMessage(string.Format(
                        TextForOutput.AnswerNumber, i, question.Text));
                }

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
            
            _stopwatch.Stop();

            var results = TestService.ExportResults(test, userAnswers,
                _stopwatch.Elapsed);

            Console.ShowMessage(results);
        }

        private static void ShowStats()
        {
            var name = Console.GetInput(TextForOutput.EnterNameToEdit);
            var title = Console.GetInput(TextForOutput.EnterTopic);

            var test = TestService.GetTest(name, title);

            Console.ShowMessage(test.ToString());
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

            TestService.AddQuestion(test, newQuestion);
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