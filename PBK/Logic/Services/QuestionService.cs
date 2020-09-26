using System;
using PBK.Entities;
using PBK.UI;

namespace PBK.Logic.Services
{
    public class QuestionService
    {
        private readonly ConsoleHandler _console;
        
        public QuestionService()
        {
            _console = new ConsoleHandler();
        }
        
        public Question GetQuestion(Test test, int questionNumber)
        {
            var newQuestion = new Question
            {
                Text = _console.GetInput(TextForOutput.EnterQuestionText),
                Number = questionNumber
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

            while (!int.TryParse(_console.GetInput(TextForOutput.EnterAnswersNumber), out attemptResult))
            {
                Console.WriteLine(TextForOutput.IncorrectInput);
            }
            var answersNumber = attemptResult;

            for (var i = 0; i < answersNumber; i++)
            {
                question.Answers.Add(_console.GetInput(TextForOutput.EnterAnswer));
            }

            question.CorrectAnswer = _console.GetInput(TextForOutput.EnterCorrectAnswer);

            if (!needGrade) return;
            while (!int.TryParse(_console.GetInput(TextForOutput.EnterQuestionScore), out attemptResult))
            {
                Console.WriteLine(TextForOutput.IncorrectInput);
            }
            question.Score = attemptResult;
        }
    }
}