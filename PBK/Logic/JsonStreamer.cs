using PBK.Test_setup;
using System.IO;
using System.Text.Json;

namespace PBK.Logic
{
    public class JsonStreamer
    {
        public static async void Write(Test info)
        {
            using (FileStream fstream = new FileStream($"{info.TestName}.json", FileMode.Create)) 
            {
                await JsonSerializer.SerializeAsync(fstream, info);
            }
        }

        public static Test Read(string name)
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
