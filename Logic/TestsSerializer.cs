using Data.Entities;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Logic
{
    public class TestsSerializer
    {
        public async void Serialize(List<Test> tests)
        {
            await using var fStream = new FileStream("Tests.json", FileMode.Open); //я не нашел как именно чистить файл, только удалять или писать поверх него

            await JsonSerializer.SerializeAsync(fStream, tests);
        }

        public List<Test> Deserialize()
        {
            using var fStream = new FileStream("Tests.json", FileMode.OpenOrCreate);
            
            return JsonSerializer.DeserializeAsync<List<Test>>(fStream).Result;
        }
    }
}