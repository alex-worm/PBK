using PBK.Entities;
using PBK.Logic.QuestionEditing;
using PBK.Logic.TopicEditing;
using PBK.UI;
using System.IO;

namespace PBK.Logic.TestEditing
{
    public class TestTool
    {
        public void DeleteTest(string name)
        {
            var writer = new ConsoleOutput();
            var testSerializer = new TestSerializer();
            var topicSerializer = new TopicSerializer();

            var test = testSerializer.Deserialize(name);

            if (test == null)
            {
                writer.PrintMessage(TextForOutput.NotOpened);

                return;
            }

            var topic = topicSerializer.Deserialize(test.TopicName);

            topic.IncludedTests.Remove(topic.IncludedTests.Find(el => el.Name == test.Name));
            topicSerializer.Serialize(topic);

            File.Delete($"{test.Name}.json");

            writer.PrintMessage(TextForOutput.FileDeleted);
        }

        public void EditTest(string name)
        {
            var writer = new ConsoleOutput();

            var testSerializer = new TestSerializer();
            var test = testSerializer.Deserialize(name);

            if (test == null)
            {
                writer.PrintMessage(TextForOutput.IncorrectInput);
                return;
            }

            if (!int.TryParse(writer.GetInput(TextForOutput.ValueToChange), out var parseResult))
            {
                writer.PrintMessage(TextForOutput.IncorrectInput);
                return;
            }

            var questionEditor = new QuestionTool();

            switch (parseResult)
            {
                case (int)ValueToEditTest.Name:
                    test.Name = writer.GetInput(TextForOutput.NewName);
                    DeleteTest(name);
                    testSerializer.Serialize(test);
                    break;

                case (int)ValueToEditTest.AddQuestion:
                    test.QuestionsNumber++;
                    questionEditor.GetQuestion(test, test.QuestionsNumber);
                    break;

                case (int)ValueToEditTest.EditQuestion:
                    while (!int.TryParse(writer.GetInput(TextForOutput.EditQuestion), out parseResult))
                    {
                        writer.PrintMessage(TextForOutput.IncorrectInput);
                    }
                    test.Questions[parseResult - 1] = questionEditor.GetQuestion(test, parseResult);
                    break;

                case (int)ValueToEditTest.TimerValue:
                    SetTimerValue(test);
                    break;

                default:
                    writer.PrintMessage(TextForOutput.IncorrectInput);
                    return;
            }

            testSerializer.Serialize(test);
        }

        public void CreateNewTest(string name)
        {
            var testSerializer = new TestSerializer();
            var topicSerializer = new TopicSerializer();
            var questionEditor = new QuestionTool();

            var test = new Test
            {
                Name = name
            };

            var testTopic = SetTopic(test);
            testTopic.IncludedTests.Add(test);

            SetQuestionsCloseness(test);
            SetQuestionsNumber(test);

            if (test.IsClosedQuestions)
            {
                SetRightAnswerIndication(test);
                SetGradeIndication(test);
            }

            for (var i = 1; i <= test.QuestionsNumber; i++)
            {
                test.Questions.Add(questionEditor.GetQuestion(test, i));
            }

            SetTimerValue(test);

            testSerializer.Serialize(test);
            topicSerializer.Serialize(testTopic);
        }

        private Topic SetTopic(BriefTestInfo test)
        {
            var writer = new ConsoleOutput();
            var topicSerializer = new TopicSerializer();

            var topicName = writer.GetInput(TextForOutput.InputTopic);
            test.TopicName = topicName;

            var topic = topicSerializer.Deserialize(topicName);

            if (topic != null)
            {
                return topic;
            }

            topic = new Topic()
            {
                Title = topicName
            };

            return topic;
        }

        private void SetQuestionsCloseness(Test test)
        {
            var writer = new ConsoleOutput();

            int.TryParse(writer.GetInput(TextForOutput.ClosedQuestions), out var result);

            switch (result)
            {
                case (int)Choice.First:
                    test.IsClosedQuestions = true;
                    break;

                case (int)Choice.Second:
                    test.IsClosedQuestions = false;
                    test.IsIndicateAnswer = false;
                    test.IsGradeAvailable = false;
                    break;

                default:
                    writer.PrintMessage(TextForOutput.IncorrectInput);
                    SetQuestionsCloseness(test);
                    break;
            }
        }

        private void SetQuestionsNumber(Test test)
        {
            var writer = new ConsoleOutput();

            if (!int.TryParse(writer.GetInput(TextForOutput.QuestionsNumber), out var result))
            {
                writer.PrintMessage(TextForOutput.IncorrectInput);
                SetQuestionsNumber(test);
            }
            else test.QuestionsNumber = result;
        }

        private void SetRightAnswerIndication(Test test)
        {
            var writer = new ConsoleOutput();

            int.TryParse(writer.GetInput(TextForOutput.IndicateAnswers), out var result);

            switch (result)
            {
                case (int)Choice.First:
                    test.IsIndicateAnswer = true;
                    break;

                case (int)Choice.Second:
                    test.IsIndicateAnswer = false;
                    break;

                default:
                    writer.PrintMessage(TextForOutput.IncorrectInput);
                    SetRightAnswerIndication(test);
                    break;
            }
        }

        private void SetGradeIndication(Test test)
        {
            var writer = new ConsoleOutput();

            int.TryParse(writer.GetInput(TextForOutput.TotalGrade), out var result);

            switch (result)
            {
                case (int)Choice.First:
                    test.IsGradeAvailable = true;
                    break;

                case (int)Choice.Second:
                    test.IsGradeAvailable = false;
                    break;

                default:
                    writer.PrintMessage(TextForOutput.IncorrectInput);
                    SetGradeIndication(test);
                    break;
            }
        }

        private void SetTimerValue(Test test)
        {
            var writer = new ConsoleOutput();

            if (!int.TryParse(writer.GetInput(TextForOutput.TimerValue), out var result))
            {
                writer.PrintMessage(TextForOutput.IncorrectInput);
                SetTimerValue(test);
            }
            else test.TimerValue = result;
        }
    }
}
