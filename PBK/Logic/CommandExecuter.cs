using PBK.Logic.EntityEditing;
using PBK.Logic.TestPassing;
using PBK.Logic.TopicEditing;
using PBK.UI;
using System;

namespace PBK.Logic
{
    class CommandExecuter
    {
        public void GetStarted()
        {
            var writer = new ConsoleOutput();

            Console.WriteLine(TextForOutput.greeting);

            ExecuteCommand(writer.DataEntry(TextForOutput.enterCommand), writer);
        }

        public void ExecuteCommand(string request, ConsoleOutput writer)
        {
            if (!int.TryParse(request, out int result))
            {
                Console.WriteLine(TextForOutput.incorrectInput);

                ExecuteCommand(writer.DataEntry(TextForOutput.enterCommand), writer);
            }

            var testEditor = new TestTool();
            var testPasser = new Tester();
            var topicViewer = new TopicTool();

            switch (result)
            {
                case (int)Command.Add:
                    testEditor.CreateNewTest();
                    break;

                case (int)Command.Edit:
                    testEditor.EditTest();
                    break;

                case (int)Command.Delete:
                    testEditor.DeleteTest(writer.DataEntry(TextForOutput.nameToDelete));
                    break;

                case (int)Command.Open:
                    testPasser.PassTest(writer.DataEntry(TextForOutput.nameToOpen));
                    break;

                case (int)Command.DisplayStats:

                    break;

                case (int)Command.Exit:
                    Environment.Exit(0);
                    break;
            }

            ExecuteCommand(writer.DataEntry(TextForOutput.enterCommand), writer);
        }
    }
}
