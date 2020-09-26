using System.Collections.Generic;

namespace PBK.Entities
{
    public class Question
    {
        private int _score;

        public Question()
        {
            Answers = new List<string>();
        }

        public string Text { get; set; }

        public int Number { get; set; }

        public List<string> Answers { get; set; }

        public string CorrectAnswer { get; set; }

        public int Score
        {
            get => _score;
            set => _score = value >= 0
                ? value
                : default;
        }
    }
}