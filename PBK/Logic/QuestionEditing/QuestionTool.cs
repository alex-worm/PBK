using PBK.Entities;
using PBK.UI;
using System;

namespace PBK.Logic.QuestionEditing
{
    public class QuestionTool
    {
        public Question GetQuestion(Test test, int questionNumber)
        {
            var writer = new ConsoleOutput();

            var newQuestion = new Question
            {
                QuestionText = writer.GetInput(TextForOutput.QuestionText),
                QuestionNumber = questionNumber
            };

            if (test.IsClosedQuestions)
            {
                SetUpClosedQuestion(newQuestion, test.IsGradeAvailable);
            }

            return newQuestion;
        }

        private void SetUpClosedQuestion(Question question, bool needGrade)
        {
            int attemptResult;
            var writer = new ConsoleOutput();

            while (!int.TryParse(writer.GetInput(TextForOutput.AnswersNumber), out attemptResult))
            {
                Console.WriteLine(TextForOutput.IncorrectInput);
            }
            question.AnswersNumber = attemptResult;

            for (var i = 0; i < question.AnswersNumber; i++)
            {
                question.Answers.Add(writer.GetInput(TextForOutput.EnterAnswer));
            }

            question.CorrectAnswer = writer.GetInput(TextForOutput.CorrectAnswer);

            if (!needGrade) return;
            while (!int.TryParse(writer.GetInput(TextForOutput.PointsNumber), out attemptResult))
            {
                Console.WriteLine(TextForOutput.IncorrectInput);
            }
            question.QuestionRating = attemptResult;
        }
    }
}
