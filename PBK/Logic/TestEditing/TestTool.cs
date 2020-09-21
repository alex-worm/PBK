using PBK.UI;
using PBK.Entities;
using System;
using System.IO;
using PBK.Logic.QuestionEditing;
using PBK.Logic.TestEditing;

namespace PBK.Logic.EntityEditing
{
    public class TestTool
    {
        public void DeleteTest(string name)
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

        public void EditTest()
        {
            var writer = new ConsoleOutput();
            var name = writer.DataEntry(TextForOutput.nameToEdit);

            var serializator = new TestSerializator();
            var test = serializator.Deserialize(name);

            if (test == null)
            {
                Console.WriteLine(TextForOutput.notOpened);
                return;
            }

            if (!int.TryParse(writer.DataEntry(TextForOutput.valueToChange), out int parseResult))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                return;
            }

            var questionEditor = new QuestionTool();

            switch (parseResult)
            {
                case (int)ValueToEditTest.Name:
                    test.Name = writer.DataEntry(TextForOutput.newName);
                    DeleteTest(name);
                    serializator.Serialize(test);
                    break;

                case (int)ValueToEditTest.Topic:
                    test.TestTopic.Title = writer.DataEntry(TextForOutput.inputTopic);
                    break;

                case (int)ValueToEditTest.CloseQuestions:
                    test.ClosedQuestions = !test.ClosedQuestions;
                    Console.WriteLine(TextForOutput.editClosedQuestions + test.ClosedQuestions);
                    break;

                case (int)ValueToEditTest.AddQuestion:
                    test.QuestionsNumber++;
                    questionEditor.InputQuestion(test, test.QuestionsNumber);
                    break;

                case (int)ValueToEditTest.IndicateCorrectAnswers:
                    test.IndicateCorrectAnswer = !test.IndicateCorrectAnswer;
                    Console.WriteLine(TextForOutput.editIndicateAnswers + test.IndicateCorrectAnswer);
                    break;

                case (int)ValueToEditTest.IndicateGrade:
                    test.GradeAvailability = !test.GradeAvailability;
                    Console.WriteLine(TextForOutput.editIndicateAnswers + test.GradeAvailability);
                    break;

                case (int)ValueToEditTest.EditQuestion:
                    while (!int.TryParse(writer.DataEntry(TextForOutput.editQuestion), out parseResult)) ;
                    test.Questions[parseResult - 1] = questionEditor.InputQuestion(test, parseResult);
                    break;

                case (int)ValueToEditTest.TimerValue:
                    while (!int.TryParse(writer.DataEntry(TextForOutput.timerValue), out parseResult)) ;
                    test.TimerValue = parseResult;
                    break;
                    
            }

            serializator.Serialize(test);
        }

        public void CreateNewTest()
        {
            var writer = new ConsoleOutput();
            var serializator = new TestSerializator();

            var test = new Test
            {
                Name = writer.DataEntry(TextForOutput.nameToAdd)
            };

            SetTitle(test, writer);
            CloseQuestions(test, writer);
            SetQuestionsNumber(test, writer);

            if (test.ClosedQuestions)
            {
                IndicateAnswers(test, writer);
                IndicateGrade(test, writer);
            }

            var questionEditor = new QuestionTool();

            for (var i = 1; i <= test.QuestionsNumber; i++)
            {
                test.Questions.Add(questionEditor.InputQuestion(test, i));
            }

            SetTimerValue(test, writer);

            serializator.Serialize(test);
        }

        private void SetTitle(Test test, ConsoleOutput writer) => test.TestTopic.Title = writer.DataEntry(TextForOutput.inputTopic);

        private void CloseQuestions(Test test, ConsoleOutput writer)
        {
            if (!int.TryParse(writer.DataEntry(TextForOutput.closedQuestions), out var result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);

                CloseQuestions(test, writer);
            }

            if (result == (int)Choise.SecondChoise)
            {
                test.ClosedQuestions = false;
                test.IndicateCorrectAnswer = false;
                test.GradeAvailability = false;
            }
            else test.ClosedQuestions = true;
        }

        private void SetQuestionsNumber(Test test, ConsoleOutput writer)
        {
            if (!int.TryParse(writer.DataEntry(TextForOutput.questionsNumber), out var result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                SetQuestionsNumber(test, writer);
            }
            test.QuestionsNumber = result;
        }

        private void IndicateAnswers(Test test, ConsoleOutput writer)
        {
            if (!int.TryParse(writer.DataEntry(TextForOutput.indicateAnswers), out var result) && result > 0 && result < 3)
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                SetTimerValue(test, writer);
            }
            test.IndicateCorrectAnswer = result == (int)Choise.FirstChoise;
        }

        private void IndicateGrade(Test test, ConsoleOutput writer)
        {
            if (!int.TryParse(writer.DataEntry(TextForOutput.totalGrade), out var result) && result > 0 && result < 3)
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                SetTimerValue(test, writer);
            }
            test.GradeAvailability = result == (int)Choise.FirstChoise;
        }

        private void SetTimerValue(Test test, ConsoleOutput writer)
        {
            if (!int.TryParse(writer.DataEntry(TextForOutput.timerValue), out var result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
                SetTimerValue(test, writer);
            }
            test.TimerValue = result;
        }
    }
}
