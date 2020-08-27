using System;
using System.Collections.Generic;
using System.Text;

namespace PBK.Test_setup
{
    class Question
    {
        internal string QuestionText { get; set; }

        internal List<string> Answers { get; set; }

        internal List<string> CorrectAnswers { get; set; }

        public Question()
        {
            Answers = new List<string>();
            CorrectAnswers = new List<string>();
        }
    }
}
