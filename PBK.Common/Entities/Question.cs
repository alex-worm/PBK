using System.Collections.Generic;

namespace Common.Entities
{
    public class Question
    {
        public Question()
        {
            Answers = new List<string>();
        }

        public string Text { get; set; }

        public int Number { get; set; }

        public List<string> Answers { get; set; }

        public int CorrectAnswer { get; set; }

        public int Score { get; set; }
    }
}