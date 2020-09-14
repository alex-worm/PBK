using System.IO;
using System.Text.Json;
using PBK.Entities;

namespace PBK.Logic.TopicEditing
{
    class TopicTool
    {
        private static async void Write(Topic topic)
        {
            using (FileStream fstream = new FileStream($"TOPIC {topic.Title}.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fstream, topic);
            }
        }

        private static Test Read(string name)
        {
            using (FileStream fstream = new FileStream($"{name}.json", FileMode.OpenOrCreate))
            {
                return JsonSerializer.DeserializeAsync<Test>(fstream).Result;
            }
        }


    }
}