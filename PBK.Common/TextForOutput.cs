namespace Common
{
    public static class TextForOutput
    {
        public const string Greeting = 
            "Greetings user. This program is designed to " +
            "create, edit and pass tests,\nwith the ability to" +
            " combine them into topics and subtopics";
        public const string EnterStarterCommand = 
            "Enter your command:\n1. Add test\n" +
            "2. Edit test\n3. Remove test\n4. Pass test\n5. Show stats\n6. Exit\n";
        public const string EnterNameToAdd = "Enter the name for the new test: ";
        public const string EnterNameToEdit = "Enter test's name to edit it: ";
        public const string EnterNameToRemove = "Enter test's name to remove: ";
        public const string EnterTopic = "Enter topic for this test: ";
        public const string IncorrectInput = "\aIncorrect input";
        public const string ChooseQuestionsType = 
            "Choose question's type:\n1. Closed\n2. Opened\n";
        public const string EnterQuestionsNumber = "Enter number of questions: ";
        public const string EnterAnswersNumber = 
            "Enter number of answers: ";
        public const string EnableIndicateCorrectAnswer = 
            "Enable displaying correct answers immediately after the answer?\n1. Yes\n2. No\n";
        public const string EnableShowGrade = 
            "Display grade at the end of the test?\n1. Yes\n2. No\n";
        public const string EnterTimerValue = 
            "Enter timer's value. If you do not want to do a timed test, enter 0: ";
        public const string EnterQuestionText = "Enter question's text: ";
        public const string EnterAnswer = "Enter answer for this question:";
        public const string EnterCorrectAnswer = "Enter correct answer: ";
        public const string ChooseTestValueToChange = 
            "Choose how to change:\n1. Edit name\n" +
            "2. Add Question\n3. Remove question\n4. Edit timer value\n";
        public const string EnterQuestionScore = 
            "Enter the number of points for this question: ";
        public const string EnterNewName = "Enter new test's name: ";
        public const string EnterQuestionNumber = "Enter number of question to change: ";
        public const string PassIsEnded = "Your attempt is over";
        public const string AnswerNumber = "{0}. {1}";
        public const string JsonName = @"Tests.json";
        public const string ClosedTitleStats =
            "Title: {0}\nPasses number: {1}\n" +
            "Total correct answers: {2}\nTotal incorrect answers: {3}\n\n";
        public const string OpenedTitleStats =
            "Topic: {0}\nPasses number: {1}\n\n";
        public const string UserAnswersTxt = @"{0}({1}).txt";
        public const string UserAnswer = "{0}: {1}";
        public const string PassTime = "Pass time: {0}";
        public const string FullResult =
            "\nGrade: {0}\nCorrect: {1}\nIncorrect: {2}\nPass time: {3}\n\n";
    }
}