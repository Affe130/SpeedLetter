using System;
using Newtonsoft.Json;
using System.IO;

namespace SpeedLetter
{
    class Settings
    {
        public string Token { get; set; }
        public string CommandPrefix { get; set; }

        public static Settings LoadFromJson(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Settings>(json);
        }

        public void SaveToJson(string path)
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, json);
        }
    }
}
