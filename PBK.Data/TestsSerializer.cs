using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Common;
using Common.Entities;

namespace Data
{
    public class TestsSerializer
    {
        public async void Serialize(List<Test> tests)
        {
            await File.WriteAllTextAsync(TextForOutput.JsonName, string.Empty);
            
            await using var fStream = new FileStream(TextForOutput.JsonName, FileMode.Open);

            await JsonSerializer.SerializeAsync(fStream, tests);
        }

        public List<Test> Deserialize()
        {
            using var fStream = new FileStream(TextForOutput.JsonName, FileMode.OpenOrCreate);
            
            return new FileInfo(TextForOutput.JsonName).Length == 0 
                ? new List<Test>() 
                : JsonSerializer.DeserializeAsync<List<Test>>(fStream).Result;
        }
    }
}