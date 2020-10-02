using System.Collections.Generic;

namespace Common.Entities
{
    public class Test
    {
        public Test()
        {
            Questions = new List<Question>();
        }
        
        public string Name { get; set; }

        public string Title { get; set; }
        
        public int TimerValue { get; set; }
        
        public bool IsClosedQuestions{get;set;}
        
        public bool IsIndicateAnswers { get; set; }

        public bool IsScoreShown { get; set; }
        
        public int QuestionsNumber { get; set; }

        public List<Question> Questions { get; set; }

        public int TotalCorrectAnswers { get; set; }

        public int TotalIncorrectAnswers { get; set; }

        public int PassesNumber { get; set; }

        public override string ToString()
        {
            if(IsClosedQuestions)
            {
                return string.Concat(TextForOutput.ClosedTitleStats, Title,
                PassesNumber,TotalCorrectAnswers,TotalIncorrectAnswers);
            }

            return string.Concat(TextForOutput.OpenedTitleStats, Title, PassesNumber);
        }
    }
}