using PBK.Entities;
using System.IO;
using System.Text.Json;

namespace PBK.Logic.TestEditing
{
    public class TestSerializer : ISerializer<Test>
    {
        public async void Serialize(Test test)
        {
            using (var fStream = new FileStream($"{test.Name}.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fStream, test);
            }
        }

        public Test Deserialize(string name)
        {
            var fileName = $"{name}.json";

            if (!File.Exists(fileName))
            {
                return null;
            }

            using (var fStream = new FileStream(fileName, FileMode.Open))
            {
                return JsonSerializer.DeserializeAsync<Test>(fStream).Result;
            }
        }
    }
}
