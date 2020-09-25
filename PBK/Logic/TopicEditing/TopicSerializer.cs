using PBK.Entities;
using System.IO;
using System.Text.Json;

namespace PBK.Logic.TopicEditing
{
    public class TopicSerializer : ISerializer<Topic>
    {
        public async void Serialize(Topic topic)
        {
            using (var fStream = new FileStream($"TOPIC {topic.Title}.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fStream, topic);
            }
        }

        public Topic Deserialize(string name)
        {
            var fileName = $"TOPIC {name}.json";

            if (!File.Exists(fileName))
            {
                return null;
            }

            using (var fStream = new FileStream(fileName, FileMode.Open))
            {
                return JsonSerializer.DeserializeAsync<Topic>(fStream).Result;
            }
        }
    }
}
