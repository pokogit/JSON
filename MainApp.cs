using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


namespace JSON
{
    class MainApp
    {
        private static StreamWriter sw;
        static void Main(string[] args)
        {
            Car car = new Car
            {
                Name = "Abarth",
                ExpirationDate = new DateTime(2019, 10, 17),
                Sizes = new string[] { "Small", "Medium", "Large" },
                HasWheels = true,
                Price = 17000M,
                NumberOfKilometers = 4888
            };

            Account account = new Account
            {
                Email = "james@example.com",
                Active = true,
                CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                Roles = new List<string>
                {
                    "User",
                    "Admin"
                }
            };

            string jsonOutput = JsonConvert.SerializeObject(car);
            WriteJson2Disc(jsonOutput);

            //jsonOutput = JsonConvert.SerializeObject(account);
            //WriteJson2Disc(jsonOutput);

            FlushAndCloseStreamWriter();

            Car deserializedProduct = JsonConvert.DeserializeObject<Car>(jsonOutput);

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            using (StreamWriter sw = new StreamWriter(@"c:\temp\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, car);
                // {"ExpiryDate":new Date(1230375600000),"Price":0}
            }

            string json = @"{
              'Name': 'Bad Boys',
              'ReleaseDate': '1995-4-7T00:00:00',
              'Genres': [
                'Action',
                'Comedy'
                  ]
                }";

            Movie m = JsonConvert.DeserializeObject<Movie>(json);
            Console.WriteLine(m.Name);
            Console.WriteLine(m.ReleaseDate);
            foreach (var genre in m.Genres)
            {
                Console.WriteLine(genre);
            }

            Linq2Json();
            Console.ReadKey();

        }

        private static void WriteJson2Disc(string jsonOutput)
        {
            sw = new StreamWriter(@"c:\temp\output.json");
            sw.Write(jsonOutput);
        }

        private static void FlushAndCloseStreamWriter()
        {
            sw.Flush();
            sw.Close();
        }

        private static void Linq2Json()
        {

            JArray array = new JArray();
            array.Add("Manual text");
            array.Add(new DateTime(2000, 5, 23));

            JObject obj = new JObject();
            obj["MyArray"] = array;

            string json = obj.ToString();

            Console.WriteLine(json);
        }
    }
}
