using System.Collections.Generic;

namespace PBK.Entities
{
    public class Test : BriefTestInfo
    {
        private int _timerValue;
        private int _questionsNumber;

        public Test()
        {
            Questions = new List<Question>();
        }

        public int QuestionsNumber
        {
            get => _questionsNumber;
            set => _questionsNumber = value >= 0
                ? value
                : default;
        }

        public bool IsClosedQuestions { get; set; }

        public List<Question> Questions { get; set; }

        public bool IsIndicateAnswer { get; set; }

        public int TimerValue
        {
            get => _timerValue;
            set => _timerValue = value >= 0
                ? value
                : default;
        }

        public bool IsGradeAvailable { get; set; }
    }
}