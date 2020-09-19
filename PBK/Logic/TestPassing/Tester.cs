using PBK.Entities;
using PBK.Logic.EntityEditing;
using PBK.Logic.TestEditing;
using PBK.UI;
using System;
using System.Threading.Tasks;

namespace PBK.Logic.TestPassing
{
    class Tester
    {
        public async void Countdown(Test test)
        {
            var writer = new ConsoleOutput();

            await Task.Run(() => writer.ShowResult(test));
        }






        //exceptions





        public void PassTest(string name)
        {
            var serializator = new TestSerializator();

            var test = serializator.Deserialize(name);

            if (test == null)
            {
                Console.WriteLine(TextForOutput.notOpened);
                return;
            }

            var userCorrect = 0;
            var userGrade = 0;

            if (test.TimerValue != 0)
            {
                Countdown(test);
            }

            test.Questions.ForEach(el =>
            {
                Console.WriteLine(el.QuestionText);

                if (AnswerIsCorrect(test, el))
                {
                    userCorrect++;
                    userGrade += el.QuestionRating;
                }
            });
        }

        private bool AnswerIsCorrect(Test test, Question question)
        {
            for(var i = 0; i < question.AnswersNumber; i++)
            {
                Console.WriteLine($"{i + 1}. {question.Answers[i]}");
            }

            var userAnswer = Console.ReadLine();

            if (test.IndicateCorrectAnswer)
            {
                Console.WriteLine(userAnswer == question.CorrectAnswer);
            }

            return userAnswer == question.CorrectAnswer;
        }
    }
}
