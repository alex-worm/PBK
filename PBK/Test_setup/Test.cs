using System.Collections.Generic;

namespace PBK.Test_setup
{
    public class Test
    {
        public string TestName { get; set; }

        public int QuestionsNumber { get; set; }

        public int AnswersNumber { get; set; }

        public List<Question> Questions { get; set; }

        public Test()
        {
            Questions = new List<Question>();
        }
    }
}
