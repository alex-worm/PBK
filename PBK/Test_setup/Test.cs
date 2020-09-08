using System.Collections.Generic;

namespace PBK.Test_setup
{
    public class Test
    {
        public Test()
        {
            Questions = new List<Question>();
        }

        public string TestName { get; set; }

        public Topic TestTopic { get; set; }

        public int QuestionsNumber { get; set; }

        public bool ClosedQuestions { get; set; }

        public int AnswersNumber { get; set; }

        public List<Question> Questions { get; set; }

        public bool IndicateCorrectAnswer { get; set; }

        public int TimerValue { get; set; }

        public bool TotalGradeAvailability { get; set; }

        public int TotalCorrectAnswers { get; set; }

        public int PassesNumber { get; set; }
    }
}
