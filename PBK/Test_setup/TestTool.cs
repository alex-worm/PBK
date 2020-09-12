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
            if(!int.TryParse(Writer.DataEntry(TextForOutput.valueToChange), out int result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                return;
            }

            switch (result)
            {
                case (int)TestValueToEdit.Name:
                    _test.TestName = Writer.DataEntry(TextForOutput.newName);
                    DeleteTest(name);
                    JsonStreamer.Write(_test);
                    break;

                case (int)TestValueToEdit.Topic:
                    _test.TestTopic.Title = Writer.DataEntry(TextForOutput.inputTopic);
                    break;

                case (int)TestValueToEdit.IndicateCorrectAnswers:
                    _test.IndicateCorrectAnswer = Writer.DataEntry(TextForOutput.indicateAnswers) == _firstChoise;
                    break;

                case (int)TestValueToEdit.TimerValue:
                    while (!int.TryParse(Writer.DataEntry(TextForOutput.timerValue), out result));
                    _test.TimerValue = result;
                    break;
            }
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
            for (var i = 0; i < _test.QuestionsNumber; i++)
            {
                InputQuestion(i);
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

        private static void InputQuestion(int questionNumber)
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

            _test.Questions.Add(newQuestion);
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
    }
}
