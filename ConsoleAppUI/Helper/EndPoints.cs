using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppUI.Helper
{
    public static class EndPoints
    {
        public static Dictionary<string, string> endpoints = new Dictionary<string, string>();
        public static Dictionary<string, string> PopulateEndPoints(string filePath)
        {

            try
            {
                string json = File.ReadAllText(filePath);
                endpoints = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                endpoints.Values.ToList().ForEach(x => Console.WriteLine(x));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading appsettings.json: {ex.Message}");
            }

            return endpoints;
        }
    }
}
