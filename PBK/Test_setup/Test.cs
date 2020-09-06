using System.Collections.Generic;

namespace PBK.Test_setup
{
    public class Test
    {
        public string TestName { get; set; }

        public int TotalCorrectAnswers { get; set; }

        public int TotalWrongAnswers { get; set; }

        public int QuestionsNumber { get; set; }

        public int AnswersNumber { get; set; }

        public List<Question> Questions { get; set; }

        public int TimerValue { get; set; }

        public Test()
        {
            Questions = new List<Question>();
        }
    }
}
