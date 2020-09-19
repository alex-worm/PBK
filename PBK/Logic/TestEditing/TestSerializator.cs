using PBK.Entities;
using System.IO;
using System.Text.Json;

namespace PBK.Logic.TestEditing
{
    class TestSerializator : ISerializator<Test>
    {
        public async void Serialize(Test test)
        {
            using (FileStream fstream = new FileStream($"{test.TestName}.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync(fstream, test);
            }
        }

        public Test Deserialize(string name)
        {
            string fileName = $"{name}.json";

            if (!File.Exists(fileName))
            {
                return null;
            }

            using (FileStream fstream = new FileStream(fileName, FileMode.Open))
            {
                return JsonSerializer.DeserializeAsync<Test>(fstream).Result;
            }
        }
    }
}
