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
                NumberOfKilometers = 4888,
                NullValue = null
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

            // WriteJsonFile(car);
            car = ReadJsonFile();
           


            string movieData = @"{
            'Name': 'Bad Boys',
            'ReleaseDate': '1995-4-7T00:00:00',
            'Genres': [
            'Action',
            'Comedy'
                ]
            }";

            Movie m = JsonConvert.DeserializeObject<Movie>(movieData);
            Console.WriteLine(m.Name);
            Console.WriteLine(m.ReleaseDate);
            foreach (var genre in m.Genres)
                Console.WriteLine(genre);

            // Empty e = JsonConvert.DeserializeObject<Empty>(movieData);

            Linq2Json();
            Console.ReadKey();

        }
       
        private static void WriteJson2Disc(string jsonOutput)
        {
            sw = new StreamWriter(@"T:\PP\json\output.json");
            sw.Write(jsonOutput);
        }

        private static void FlushAndCloseStreamWriter()
        {
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Convert LINQ to JSON
        /// </summary>
        private static void Linq2Json()
        {

            JArray array = new JArray
            {
                "Manual text",
                new DateTime(2000, 5, 23)
            };

            JObject obj = new JObject();
            obj["MyArray"] = array;

            string json = obj.ToString();

            Console.WriteLine(json);
        }

        private static void WriteJsonFile(Car car)
        {
            // Way 1
            File.WriteAllText(@"c:\movie.json", JsonConvert.SerializeObject(car));

            // Way 2
            using (StreamWriter file = File.CreateText(@"c:\movie.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, car);
            }
        }

        private static Car ReadJsonFile()
        {
            // Way 1
            Car car = JsonConvert.DeserializeObject<Car>(File.ReadAllText(@"T:\PP\json\output.json"));

            // Way 2
            using (StreamReader sr = File.OpenText(@"T:\PP\json\output.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Car car2 = (Car)serializer.Deserialize(sr, typeof(Car));
            }

            return car;
        }
    }
}
