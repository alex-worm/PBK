using PBK.Test_setup;
using System;
using System.IO;
using System.Text.Json;

namespace PBK.Logic
{
    public class JsonStreamer
    {
        public static void Write(Test info)
        {
            using (FileStream fstream = new FileStream($"{info.TestName}.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.SerializeAsync(fstream, info);
            }
        }

        public static Test Read(string name)
        {
            try
            {
                using (FileStream fstream = new FileStream($"{name}.json", FileMode.Open))
                {
                    return JsonSerializer.DeserializeAsync<Test>(fstream).Result;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
