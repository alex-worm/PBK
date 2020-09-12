using System.Collections.Generic;

namespace PBK.Test_setup
{
    public class Test
    {
        private int _timerValue;
        private int _questionsNumber;

        public Test()
        {
            Questions = new List<Question>();

            TestTopic = new Topic();
            TestTopic.Subtopics = new List<Topic>();
        }

        public string TestName { get; set; }

        public Topic TestTopic { get; set; }

        public int QuestionsNumber
        {
            get => _questionsNumber;
            set => _questionsNumber = value >= 0
                ? value
                : default;
        }

        public bool ClosedQuestions { get; set; }

        public List<Question> Questions { get; set; }

        public bool IndicateCorrectAnswer { get; set; }

        public int TimerValue
        {
            get => _timerValue;
            set => _timerValue = value >= 0
                ? value
                : default;
        }

        public bool TotalGradeAvailability { get; set; }

        public int TotalCorrectAnswers { get; set; }

        public int PassesNumber { get; set; }
    }
}
