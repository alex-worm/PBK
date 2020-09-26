using PBK.Enums;
using PBK.Logic.Services;
using PBK.UI;

namespace PBK.Logic.Starter
{
    public static class Starter
    {
        private static readonly ConsoleHandler Console;
        private static readonly TestService TestService;
        private static readonly TopicService TopicService;
        
        static Starter()
        {
            Console = new ConsoleHandler();
            TestService = new TestService();
            TopicService = new TopicService();
        }

        public static void Start()
        {
            Console.ShowMessage(TextForOutput.Greeting);

            while (true)
            {
                if (!int.TryParse(Console.GetInput(TextForOutput.EnterStarterCommand),
                    out var result))
                {
                    Console.ShowMessage(TextForOutput.IncorrectInput);
                }

                switch (result)
                {
                    case (int)FirstAction.AddTest:
                        TestService.CreateTest(
                            Console.GetInput(TextForOutput.EnterNameToAdd));
                        break;

                    case (int)FirstAction.Open:
                        TestService.PassTest(
                            Console.GetInput(TextForOutput.EnterNameToOpen));
                        break;

                    case (int)FirstAction.EditTest:
                        TestService.EditTest(
                            Console.GetInput(TextForOutput.EnterNameToEdit));
                        break;

                    case (int)FirstAction.DeleteTest:
                        TestService.DeleteTest(
                            Console.GetInput(TextForOutput.EnterNameToDelete));
                        break;

                    case (int)FirstAction.DisplayStats:
                        TopicService.DisplaySummary(
                            Console.GetInput(TextForOutput.EnterNameToOpen));
                        break;

                    case (int)FirstAction.AddSubtopic:
                        TopicService.AddSubtopic(
                            Console.GetInput(TextForOutput.EnterNameToOpen));
                        break;

                    case (int)FirstAction.DeleteTopic:
                        TopicService.DeleteTopic(
                            Console.GetInput(TextForOutput.EnterNameToDelete));
                        break;

                    case (int)FirstAction.Exit:
                        return;
                }
            }
        }
    }
}