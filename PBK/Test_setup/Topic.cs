using System;
using System.Collections.Generic;
using System.Text;

namespace PBK.Test_setup
{
    public class Topic
    {
        public Topic()
        {
            Subtopics = new List<Topic>();
        }

        public string Title { get; set; }

        public List<Topic> Subtopics { get; set; }
    }
}
