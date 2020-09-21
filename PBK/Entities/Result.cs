using System;

namespace PBK.Entities
{
    public class Result
    {
        public Result()
        {
            CorrectAnswers = 0;
            IncorrectAnswers = 0;
            Grade = 0;
        }

        public TimeSpan PassTime { get; set; }

        public int CorrectAnswers { get; set; }

        public int IncorrectAnswers { get; set; }

        public int Grade { get; set; }
    }
}
