using System.Collections.Generic;

namespace PBK.Entities
{
    public class Topic
    {
        public Topic()
        {
            Subtopics = new List<Topic>();
            IncludedTests = new List<BriefTestInfo>();
        }

        public string Title { get; set; }

        public List<Topic> Subtopics { get; set; }

        public List<BriefTestInfo> IncludedTests { get; set; }

        public static Topic operator +(Topic topic, Topic subtopic)
        {
            subtopic.IncludedTests.ForEach(el=>topic.IncludedTests.Add(el));

            return topic;
        }
    }
}
