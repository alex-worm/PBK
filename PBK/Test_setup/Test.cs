using System;
using System.Collections.Generic;
using System.Text;

namespace PBK.Test_setup
{
    public class Test
    {
        internal string TestName { get; set; }

        internal int QuestionsNumber { get; set; }

        internal int AnswersNumber { get; set; }

        internal List<Question> Questions { get; set; }

        public Test()
        {
            Questions = new List<Question>();
        }
    }
}
