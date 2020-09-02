using PBK.Test_setup;
using System;
using System.IO;
using System.Linq.Expressions;
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
