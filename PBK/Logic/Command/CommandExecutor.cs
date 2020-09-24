using System;
using PBK.Logic.TestEditing;
using PBK.Logic.TestPassing;
using PBK.Logic.TopicEditing;
using PBK.UI;

namespace PBK.Logic.Command
{
    public class CommandExecutor
    {
        public void GetStarted()
        {
            var writer = new ConsoleOutput();

            writer.PrintMessage(TextForOutput.Greeting);

            ExecuteCommand();
        }

        private static void ExecuteCommand()
        {
            var writer = new ConsoleOutput();

            if (!int.TryParse(writer.GetInput(TextForOutput.EnterCommand), out var result))
            {
                writer.PrintMessage(TextForOutput.IncorrectInput);

                ExecuteCommand();
            }

            var testEditor = new TestTool();
            var testPasser = new Tester();
            var topicEditor = new TopicTool();

            switch (result)
            {
                case (int)Logic.Command.Command.AddTest:
                    testEditor.CreateNewTest(writer.GetInput(TextForOutput.NameToAdd));
                    break;

                case (int)Logic.Command.Command.Open:
                    testPasser.PassTest(writer.GetInput(TextForOutput.NameToOpen));
                    break;

                case (int)Logic.Command.Command.EditTest:
                    testEditor.EditTest(writer.GetInput(TextForOutput.NameToEdit));
                    break;

                case (int)Logic.Command.Command.DeleteTest:
                    testEditor.DeleteTest(writer.GetInput(TextForOutput.NameToDelete));
                    break;

                case (int)Logic.Command.Command.DisplayStats:
                    topicEditor.DisplaySummary(writer.GetInput(TextForOutput.NameToOpen));
                    break;

                case (int)Logic.Command.Command.AddSubtopic:
                    topicEditor.AddSubtopic(writer.GetInput(TextForOutput.NameToOpen));
                    break;

                case (int)Logic.Command.Command.DeleteTopic:
                    topicEditor.DeleteTopic(writer.GetInput(TextForOutput.NameToDelete));
                    break;

                case (int)Logic.Command.Command.Exit:
                    Environment.Exit(0);
                    return;

                default:
                    break;
            }

            ExecuteCommand();
        }
    }
}
