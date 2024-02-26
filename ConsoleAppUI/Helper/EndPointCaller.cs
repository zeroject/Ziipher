using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppUI.Helper
{
    public static class EndPointCaller
    {
        public static async Task<HttpResponseMessage> CallPostEndpointAsync(string endpoint, Object data)
        {
            HttpClient client = new HttpClient();
            var response = await client.PostAsJsonAsync(endpoint, data);
            Console.WriteLine(response);
            return response; // Simulating a successful operation
        }
    }
}
