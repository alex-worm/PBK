using PBK.UI;
using PBK.Logic;
using System;
using System.IO;

namespace PBK.Test_setup
{
    public class TestTool
    {
        private static Test _test;
        private const string _firstChoise = "1";

        public static void DeleteTest(string name)
        {
            string fileName = $"{name}.json";

            if (!File.Exists(fileName))
            {
                Console.WriteLine(TextForOutput.notOpened);
                return;
            }

            File.Delete(fileName);
            Console.WriteLine(TextForOutput.fileDeleted);
        }

        public static void OpenTest()
        {
            var name = Writer.DataEntry(TextForOutput.nameToOpen);
            _test = JsonStreamer.Read(name);

            if (_test == null)
            {
                Console.WriteLine(TextForOutput.notOpened);
                return;
            }

            TestTimer.Countdown(_test);
        }

        public static void EditTest()
        {
            var name = Writer.DataEntry(TextForOutput.nameToEdit);
            _test = JsonStreamer.Read(name);

            if (_test == null)
            {
                Console.WriteLine(TextForOutput.notOpened);
                return;
            }
            if (!int.TryParse(Writer.DataEntry(TextForOutput.valueToChange), out int parseResult))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                return;
            }

            switch (parseResult)
            {
                case (int)TestValueToEdit.Name:
                    _test.TestName = Writer.DataEntry(TextForOutput.newName);
                    DeleteTest(name);
                    JsonStreamer.Write(_test);
                    break;

                case (int)TestValueToEdit.Topic:
                    _test.TestTopic.Title = Writer.DataEntry(TextForOutput.inputTopic);
                    break;

                case (int)TestValueToEdit.CloseQuestions:
                    _test.ClosedQuestions = !_test.ClosedQuestions;
                    Console.WriteLine(TextForOutput.editClosedQuestions + _test.ClosedQuestions);
                    break;

                case (int)TestValueToEdit.AddQuestion:
                    _test.QuestionsNumber++;
                    InputQuestion(_test.QuestionsNumber);
                    break;

                case (int)TestValueToEdit.IndicateCorrectAnswers:
                    _test.IndicateCorrectAnswer = !_test.IndicateCorrectAnswer;
                    Console.WriteLine(TextForOutput.editIndicateAnswers + _test.IndicateCorrectAnswer);
                    break;

                case (int)TestValueToEdit.IndicateGrade:
                    _test.TotalGradeAvailability = !_test.TotalGradeAvailability;
                    Console.WriteLine(TextForOutput.editIndicateAnswers + _test.TotalGradeAvailability);
                    break;

                case (int)TestValueToEdit.EditQuestion:
                    while (!int.TryParse(Writer.DataEntry(TextForOutput.editQuestion), out parseResult)) ;
                    _test.Questions[parseResult - 1] = InputQuestion(parseResult);
                    break;

                case (int)TestValueToEdit.TimerValue:
                    while (!int.TryParse(Writer.DataEntry(TextForOutput.timerValue), out parseResult)) ;
                    _test.TimerValue = parseResult;
                    break;
                    
            }

            JsonStreamer.Write(_test);
        }

        public static void CreateNewTest()
        {
            _test = new Test
            {
                TestName = Writer.DataEntry(TextForOutput.nameToAdd)
            };
            SetTitle();
            CloseQuestions();
            SetQuestionsNumber();
            if (_test.ClosedQuestions)
            {
                IndicateAnswers();
                IndicateGrade();
            }
            for (var i = 1; i <= _test.QuestionsNumber; i++)
            {
                _test.Questions.Add(InputQuestion(i));
            }
            SetTimerValue();

            JsonStreamer.Write(_test);
        }

        private static void SetTitle() => _test.TestTopic.Title = Writer.DataEntry(TextForOutput.inputTopic);

        private static void CloseQuestions()
        {
            if (Writer.DataEntry(TextForOutput.closedQuestions) != _firstChoise)
            {
                _test.ClosedQuestions = false;
                _test.IndicateCorrectAnswer = false;
                _test.TotalGradeAvailability = false;
            }
            else _test.ClosedQuestions = true;
        }

        private static void SetQuestionsNumber()
        {
            if (!int.TryParse(Writer.DataEntry(TextForOutput.questionsNumber), out var result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                SetQuestionsNumber();
            }
            _test.QuestionsNumber = result;
        }

        private static void IndicateAnswers()=> _test.IndicateCorrectAnswer = Writer.DataEntry(TextForOutput.indicateAnswers) == _firstChoise;

        private static void IndicateGrade() => _test.TotalGradeAvailability = Writer.DataEntry(TextForOutput.totalGrade) == _firstChoise;

        private static Question InputQuestion(int questionNumber)
        {
            Question newQuestion = new Question
            {
                QuestionText = Writer.DataEntry(TextForOutput.questionText),
                QuestionNumber = questionNumber
            };

            if (_test.ClosedQuestions)
            {
                SetUpClosedQuestion(newQuestion);
            }

            return newQuestion;
        }

        private static void SetUpClosedQuestion(Question question)
        {
            int attemptResult;

            while (!int.TryParse(Writer.DataEntry(TextForOutput.answersNumber), out attemptResult))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
            }
            question.AnswersNumber = attemptResult;

            for (var i = 0; i < question.AnswersNumber; i++)
            {
                question.Answers.Add(Writer.DataEntry(TextForOutput.enterAnswer));
            }

            foreach (var answer in Writer.DataEntry(TextForOutput.correctAnswers)
                .Split(" ,.\t".ToCharArray()))
            {
                question.CorrectAnswers.Add(answer);
            }

            while (!int.TryParse(Writer.DataEntry(TextForOutput.pointsNumber), out attemptResult))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
            }
            question.QuestionRating = attemptResult;
        }

        private static void SetTimerValue()
        {
            if (!int.TryParse(Writer.DataEntry(TextForOutput.timerValue), out var result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                SetTimerValue();
            }
            _test.TimerValue = result;
        }

        public static void ShowResult(object test)
        {
            _test = (Test)test;
            _test.PassesNumber++;
            Console.WriteLine(_test.TestName);
        }
    }
}
