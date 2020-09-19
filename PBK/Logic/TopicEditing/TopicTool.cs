using System;
using System.IO;
using PBK.UI;

namespace PBK.Logic.TopicEditing
{
    class TopicTool
    {
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

        public void DisplaySummary(string name)
        {
            if (!CheckFileExistence(name))
            {
                Console.WriteLine(TextForOutput.notOpened);
            }
            var serializator = new TopicSerializator();
            var topic = serializator.Deserialize(name);

            if (topic == null)
            {
                Console.WriteLine(TextForOutput.notOpened);
                return;
            }

            var writer = new ConsoleOutput();

            writer.DisplayTopicInfo(topic);
        }
    }
}