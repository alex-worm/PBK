﻿using PBK.UI;
using System.IO;

namespace PBK.Logic.TopicEditing
{
    public class TopicTool
    {
        public void DeleteTopic(string name)
        {
            var writer = new ConsoleOutput();
            var topicSerializer = new TopicSerializer();

            var topic = topicSerializer.Deserialize(name);

            if (topic == null)
            {
                writer.PrintMessage(TextForOutput.IncorrectInput);

                return;
            }

            topic.IncludedTests.ForEach(el =>
            {
                File.Delete($"{el.Name}.json");
            });

            File.Delete($"TOPIC {topic.Title}.json");

            writer.PrintMessage(TextForOutput.FileDeleted);
        }

        public void AddSubtopic(string name)
        {
            var writer = new ConsoleOutput();
            var topicSerializer = new TopicSerializer();

            var topic = topicSerializer.Deserialize(name);

            if (topic == null)
            {
                writer.PrintMessage(TextForOutput.NotOpened);
                return;
            }

            var subtopic = topicSerializer
                .Deserialize(writer.GetInput(TextForOutput.AddSubtopic));

            topic.Subtopics.Add(subtopic);

            topicSerializer.Serialize(topic);
        }

        public void DisplaySummary(string name)
        {
            var writer = new ConsoleOutput();
            var topicSerializer = new TopicSerializer();

            var topic = topicSerializer.Deserialize(name);

            if (topic == null)
            {
                writer.PrintMessage(TextForOutput.NotOpened);
                return;
            }

            topic.Subtopics.ForEach(el => topic += el);

            writer.PrintTopicStats(topic);
        }
    }
}