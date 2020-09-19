using PBK.Entities;
using PBK.UI;
using System;


namespace PBK.Logic.QuestionEditing
{
    class QuestionTool
    {
        public static Question InputQuestion(Test test, int questionNumber)
        {
            Question newQuestion = new Question
            {
                QuestionText = Writer.DataEntry(TextForOutput.questionText),
                QuestionNumber = questionNumber
            };

            if (test.ClosedQuestions)
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

            question.CorrectAnswer = Writer.DataEntry(TextForOutput.correctAnswer);

            while (!int.TryParse(Writer.DataEntry(TextForOutput.pointsNumber), out attemptResult))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
            }
            question.QuestionRating = attemptResult;
        }
    }
}
