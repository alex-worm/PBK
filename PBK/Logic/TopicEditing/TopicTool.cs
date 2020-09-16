using System;
using System.IO;
using System.Text.Json;
using PBK.Entities;
using PBK.UI;

namespace PBK.Logic.TopicEditing
{
    class TopicTool
    {
        private static Topic _topic;

        private static bool CheckFileExistence(string fileName)
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            return false;
        }

        public static void DeleteTopic(string fileName)
        {          
            if (!CheckFileExistence(fileName))
            {
                Console.WriteLine(TextForOutput.notOpened);
            }

            File.Delete(fileName);
            Console.WriteLine(TextForOutput.fileDeleted);
        }

        public static void DisplaySummary(string name)
        {
            if (!CheckFileExistence(name))
            {
                Console.WriteLine(TextForOutput.notOpened);
            }

            _topic = Read(name);

            if (_topic == null)
            {
                Console.WriteLine(TextForOutput.notOpened);
                return;
            }

            Writer.DisplayTopicInfo(_topic);
        }

        private static async void Write(Topic topic)
        {
            using (FileStream fstream = new FileStream($"TOPIC {topic.Title}.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fstream, topic);
            }
        }

        private static Topic Read(string fileName)
        {
            using (FileStream fstream = new FileStream($"{fileName}.json", FileMode.OpenOrCreate))
            {
                return JsonSerializer.DeserializeAsync<Topic>(fstream).Result;
            }
        }
    }
}