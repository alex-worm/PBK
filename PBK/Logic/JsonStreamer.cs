using PBK.Test_setup;
using System.IO;
using System.Text.Json;

namespace PBK.Logic
{
    class JsonStreamer
    {
        internal static void Write(Test info)
        {
            using (FileStream fstream = new FileStream($"{info.TestName}.json", FileMode.Create))
            {
                JsonSerializer.SerializeAsync(fstream, info);
            }
        }

        internal static string Read(string name)
        {
            using (FileStream fstream = new FileStream($"{name}.json", FileMode.Open))
            {
                return JsonSerializer.DeserializeAsync<string>(fstream).Result;
            }
        }
    }
}
