﻿namespace PBK.UI
{
    public static class TextForOutput
    {
        public const string Greeting = "Greetings user. This program is designed to create, edit and pass tests,\n" +
                                       "with the ability to combine them into topics and subtopics";
        public const string EnterStarterCommand = "Enter your command:\n1. Add test\n2. Open test\n3. Edit test\n4. Delete test\n" +
                                           "5. Display topic stats\n6. Add subtopic\n7. Delete topic\n8. Exit\n";
        public const string EnterNameToAdd = "Enter the name for the new test: ";
        public const string EnterNameToEdit = "Enter file's name to edit it: ";
        public const string EnterNameToDelete = "Enter file's name to delete: ";
        public const string EnterNameToOpen = "Enter file's name to open: ";
        public const string NotOpened = "\aFile not opened";
        public const string FileDeleted = "File deleted successfully";
        public const string EnterTopic = "Enter topic for this test: ";
        public const string IncorrectInput = "\aIncorrect input";
        public const string ChooseQuestionsType = "Choose question's type:\n1. Closed\n2. Opened\n";
        public const string EnterQuestionsNumber = "Enter number of questions: ";
        public const string EnterAnswersNumber = "Enter number of answers: ";
        public const string EnableIndicateCorrectAnswer = "Enable displaying correct answers immediately after the answer?\n" +
                                              "1. Yes\n2. No\n";
        public const string EnableShowGrade = "Display grade at the end of the test?\n1. Yes\n2. No\n";
        public const string EnterTimerValue = "Enter timer's value. If you do not want to do a timed test, enter 0: ";
        public const string EnterQuestionText = "Enter question's text: ";
        public const string EnterAnswer = "Enter answer for this question:";
        public const string EnterCorrectAnswer = "Enter correct answer: ";
        public const string ChooseTestValueToChange = "Choose a value to change:\n1. Name\n" +
                                            "2. Number of questions\n3. Number of answers\n4. Question\n";
        public const string EnterQuestionScore = "Enter the number of points for this question: ";
        public const string EnterNewName = "Enter new test's name: ";
        public const string EnterQuestionNumber = "Enter number of question to change: ";
        public const string PassIsEnded = "Your attempt is over";
        public const string EnterSubtopicName = "Enter topic's name to make it subtopic: ";
    }
}