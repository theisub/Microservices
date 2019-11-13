using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway
{
    public class JsonWorker
    {
        public static T LoadFromFile<T>(string filePath)
        {

            using (StreamReader reader = new StreamReader(filePath))
            {

                string json = reader.ReadToEnd();
                T Result = JsonConvert.DeserializeObject<T>(json);
                return Result;
            }
        }

        public static T Deserialize<T>(object jsonObject)
        {
            return JsonConvert.DeserializeObject<T>(Convert.ToString(jsonObject));
        }
    }
}
