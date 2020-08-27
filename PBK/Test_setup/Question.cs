using System;
using System.Collections.Generic;
using System.Text;

namespace PBK.Test_setup
{
    public class Question
    {
        public string QuestionText { get; set; }

        public List<string> Answers { get; set; }

        public List<string> CorrectAnswers { get; set; }

        public Question()
        {
            Answers = new List<string>();
            CorrectAnswers = new List<string>();
        }
    }
}
