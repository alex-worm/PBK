using System.Collections.Generic;

namespace PBK.Test_setup
{
    public class Question
    {
        public Question()
        {
            Answers = new List<string>();
            CorrectAnswers = new List<string>();
        }

        public string QuestionText { get; set; }

        public int QuestionNumber { get; set; }

        public List<string> Answers { get; set; }

        public List<string> CorrectAnswers { get; set; }

        public int QuestionRating { get; set; }
    }
}
