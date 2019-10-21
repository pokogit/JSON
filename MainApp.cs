using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JSON.Classes;
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

            //SerializeObject();
            //SerializeConditionalProperty();
            //ConstructorHandling();
            Kreditkarte();

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

        private static void SerializeObject()
        {
            List<string> videogames = new List<string>
            {
                "Starcraft",
                "Halo",
                "Legend of Zelda"
            };

            string json = JsonConvert.SerializeObject(videogames);

            WriteJsonOutputToConsole(MethodBase.GetCurrentMethod().ToString(), json);
        }

        private static void SerializeConditionalProperty()
        {
            Employee joe = new Employee();
            joe.Name = "Joe Employee";
            Employee mike = new Employee();
            mike.Name = "Mike Manager";

            joe.Manager = mike;

            // mike is his own manager
            // ShouldSerialize will skip this property
            mike.Manager = mike;

            string json = JsonConvert.SerializeObject(new[] { joe, mike }, Formatting.Indented);

            WriteJsonOutputToConsole(MethodBase.GetCurrentMethod().ToString(), json);

        }

        private static void ConstructorHandling()
        {
            string json = @"{'Url':'http://www.google.com'}";

            //try
            //{
            //    JsonConvert.DeserializeObject<Website>(json);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    // Value cannot be null.
            //    // Parameter name: website
            //}

            try
            {
                Website website = JsonConvert.DeserializeObject<Website>(json, 
                    new JsonSerializerSettings { ConstructorHandling = Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor });

                Console.WriteLine(website.Url);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Kreditkarte()
        {
            Kreditkarte kreditkarte = new Kreditkarte();
            Inhaber inhaber = new Inhaber();

            kreditkarte.Herausgeber = "XEMA";
            kreditkarte.Nummer = "1234-5678-9012-3456";
            kreditkarte.Deckung = 2e+6;
            kreditkarte.Waehrung = "EURO";
            inhaber.Name = "Mustermann";
            inhaber.Vorname = "Max";
            inhaber.Maennlich = true;
            inhaber.Hobbys = new string[] { "Reiten", "Golfen", "Lesen" };
            inhaber.Alter = 42;
            inhaber.Kinder = new int[] { };
            kreditkarte.Inhaber = inhaber;
            string json = JsonConvert.SerializeObject(kreditkarte, Formatting.Indented);
            WriteJsonOutputToConsole(MethodBase.GetCurrentMethod().ToString(), json);
        }



        private static void WriteJsonOutputToConsole(string methodName, string result)
        {
            Console.WriteLine(string.Format("{0} --> {1}{2}", methodName, Environment.NewLine, result));
            Console.ReadKey();
        }

    }
}
