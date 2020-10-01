using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Common.Entities;

namespace Data
{
    public class TestsSerializer
    {
        public async void Serialize(List<Test> tests)
        {
            await using var fStream = new FileStream("Tests.json", FileMode.Open);

            await JsonSerializer.SerializeAsync(fStream, tests);
        }

        public List<Test> Deserialize()
        {
            if (!File.Exists("Tests.json"))
            {
                return new List<Test>();
            }

            using var fStream = new FileStream("Tests.json", FileMode.OpenOrCreate);          
            
            return JsonSerializer.DeserializeAsync<List<Test>>(fStream).Result;
        }
    }
}