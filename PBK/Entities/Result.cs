﻿using System;
using System.Collections.Generic;

namespace PBK.Entities
{
    public class Result
    {
        public Result()
        {
            UserAnswers = new List<string>();
        }

        public TimeSpan PassTime { get; set; }

        public int CorrectAnswers { get; set; }

        public int IncorrectAnswers { get; set; }

        public int Grade { get; set; }

        public List<string> UserAnswers { get; set; }
    }
}