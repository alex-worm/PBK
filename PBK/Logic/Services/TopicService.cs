using System.IO;
using PBK.Logic.Serializers;
using PBK.UI;

namespace PBK.Logic.Services
{
    public class TopicService
    {
        private readonly ConsoleHandler _console;
        private readonly TopicSerializer _topicSerializer;

        public TopicService()
        {
            _console = new ConsoleHandler();
            _topicSerializer = new TopicSerializer();
        }

        public void DeleteTopic(string name)
        {
            var topic = _topicSerializer.Deserialize(name);

            if (topic == null)
            {
                _console.ShowMessage(TextForOutput.IncorrectInput);

                return;
            }

            topic.IncludedTests.ForEach(el =>
            {
                File.Delete($"{el.Name}.json");
            });

            File.Delete($"TOPIC {topic.Title}.json");

            _console.ShowMessage(TextForOutput.FileDeleted);
        }

        public void AddSubtopic(string name)
        {
            var topic = _topicSerializer.Deserialize(name);

            if (topic == null)
            {
                _console.ShowMessage(TextForOutput.NotOpened);
                return;
            }

            var subtopic = _topicSerializer
                .Deserialize(_console.GetInput(TextForOutput.EnterSubtopicName));

            topic.Subtopics.Add(subtopic);

            _topicSerializer.Serialize(topic);
        }

        public void DisplaySummary(string name)
        {
            var topic = _topicSerializer.Deserialize(name);

            if (topic == null)
            {
                _console.ShowMessage(TextForOutput.NotOpened);
                return;
            }

            topic.Subtopics.ForEach(el => topic += el);

            _console.ShowTopicStats(topic);
        }
    }
}