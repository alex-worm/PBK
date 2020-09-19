using PBK.Entities;
using System.IO;
using System.Text.Json;

namespace PBK.Logic.TopicEditing
{
    class TopicSerializator : ISerializator<Topic>
    {
        public async void Serialize(Topic topic)
        {
            using (FileStream fstream = new FileStream($"TOPIC {topic.Title}.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fstream, topic);
            }
        }

        public Topic Deserialize(string fileName)
        {
            using (FileStream fstream = new FileStream($"{fileName}.json", FileMode.OpenOrCreate))
            {
                return JsonSerializer.DeserializeAsync<Topic>(fstream).Result;
            }
        }
    }
}
