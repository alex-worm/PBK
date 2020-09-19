using PBK.Entities;
using PBK.UI;
using System;

namespace PBK.Logic.QuestionEditing
{
    class QuestionTool
    {
        public Question InputQuestion(Test test, int questionNumber)
        {
            var writer = new ConsoleOutput();

            Question newQuestion = new Question
            {
                QuestionText = writer.DataEntry(TextForOutput.questionText),
                QuestionNumber = questionNumber
            };

            if (test.ClosedQuestions)
            {
                SetUpClosedQuestion(newQuestion, writer);
            }

            return newQuestion;
        }

        private void SetUpClosedQuestion(Question question, ConsoleOutput writer)
        {
            int attemptResult;

            while (!int.TryParse(writer.DataEntry(TextForOutput.answersNumber), out attemptResult))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
            }
            question.AnswersNumber = attemptResult;

            for (var i = 0; i < question.AnswersNumber; i++)
            {
                question.Answers.Add(writer.DataEntry(TextForOutput.enterAnswer));
            }

            question.CorrectAnswer = writer.DataEntry(TextForOutput.correctAnswer);

            while (!int.TryParse(writer.DataEntry(TextForOutput.pointsNumber), out attemptResult))
            {
                Console.WriteLine(TextForOutput.incorrectInput);
            }
            question.QuestionRating = attemptResult;
        }
    }
}
