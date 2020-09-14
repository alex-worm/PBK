using System.Collections.Generic;

namespace PBK.Entities
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
