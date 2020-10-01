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
            await using var fStream = new FileStream("Tests.json", FileMode.Create);

            await JsonSerializer.SerializeAsync(fStream, tests);
        }

        public List<Test> Deserialize()
        {
            using var fStream = new FileStream("Tests.json", FileMode.OpenOrCreate);
            
            return new FileInfo("Tests.json").Length == 0 
                ? new List<Test>() 
                : JsonSerializer.DeserializeAsync<List<Test>>(fStream).Result;
        }
    }
}