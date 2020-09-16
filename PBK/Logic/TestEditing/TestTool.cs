using PBK.UI;
using PBK.Entities;
using System;
using System.IO;
using System.Text.Json;
using PBK.Logic.QuestionEditing;

namespace PBK.Logic.EntityEditing
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

        public static void EditTest()
        {
            var name = Writer.DataEntry(TextForOutput.nameToEdit);
            _test = Read(name);

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
                    Write(_test);
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
                    QuestionTool.InputQuestion(_test, _test.QuestionsNumber);
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
                    _test.Questions[parseResult - 1] = QuestionTool.InputQuestion(_test, parseResult);
                    break;

                case (int)TestValueToEdit.TimerValue:
                    while (!int.TryParse(Writer.DataEntry(TextForOutput.timerValue), out parseResult)) ;
                    _test.TimerValue = parseResult;
                    break;
                    
            }

            Write(_test);
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
                _test.Questions.Add(QuestionTool.InputQuestion(_test, i));
            }
            SetTimerValue();

            Write(_test);
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

        private static void SetTimerValue()
        {
            if (!int.TryParse(Writer.DataEntry(TextForOutput.timerValue), out var result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                SetTimerValue();
            }
            _test.TimerValue = result;
        }

        public static async void Write(Test test)
        {
            using (FileStream fstream = new FileStream($"{test.TestName}.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fstream, test);
            }
        }

        public static Test Read(string name)
        {
            string fileName = $"{name}.json";

            if (!File.Exists(fileName))
            {
                return null;
            }

            using (FileStream fstream = new FileStream(fileName, FileMode.Open))
            {
                return JsonSerializer.DeserializeAsync<Test>(fstream).Result;
            }
        }
    }
}
