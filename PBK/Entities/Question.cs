﻿using System.Collections.Generic;

namespace PBK.Entities
{
    public class Question
    {
        private int _questionRating;
        private int _answersNumber;

        public Question()
        {
            Answers = new List<string>();
        }

        public string QuestionText { get; set; }

        public int QuestionNumber { get; set; }

        public List<string> Answers { get; set; }

        public string CorrectAnswer { get; set; }

        public int AnswersNumber
        {
            get => _answersNumber;
            set => _answersNumber = value >= 0
                ? value
                : default;
        }

        public int QuestionRating
        {
            get => _questionRating;
            set => _questionRating = value >= 0
                ? value
                : default;
        }
    }
}
