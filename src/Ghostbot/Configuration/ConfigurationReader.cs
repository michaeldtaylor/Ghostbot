using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ghostbot.Configuration
{
    public static class ConfigurationReader
    {
        public static T Read<T>(string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                return Read<T>(fileStream);
            }
        }

        static T Read<T>(Stream stream)
        {
            using (var jsonTextReader = new JsonTextReader(new StringReader(GetSpecificJson<T>(stream))))
            {
                return JsonSerializer.CreateDefault().Deserialize<T>(jsonTextReader);
            }
        }

        static string GetSpecificJson<T>(Stream stream)
        {
            var name = typeof(T).Name;

            using (var jsonTextReader = new JsonTextReader(new StreamReader(stream)))
            {
                var jsonConfigurationObject = JsonSerializer.CreateDefault().Deserialize<JsonConfigurationObject>(jsonTextReader);

                return jsonConfigurationObject.Configuration.ContainsKey(name) ? jsonConfigurationObject.Configuration[name].ToString() : string.Empty;
            }
        }

        class JsonConfigurationObject
        {
            [JsonExtensionData]
            public Dictionary<string, JToken> Configuration { get; set; }
        }
    }
}
