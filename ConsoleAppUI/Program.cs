using AuthenticationService.Dto;
using Domain;
using Domain.DTO_s;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class Program
{
    #region Static Variables
    public static bool run = true;
    public static string token = "";

    static Dictionary<string, string> commands = new Dictionary<string, string>{
        {"/help", "Displays all commands"},
        {"/exit", "Exits the program"},
        {"/login", "Adds a new item to the list"},
        {"/logout", "Removes an item from the list"}
    };
    #endregion

    static void Main(string[] args)
    {
        string jsonFilePath = "appsettings.json";
        Dictionary<string, string> endpoints = ReadAppSettings(jsonFilePath);

        while (run)
        {
            string username;
            string password;
            string input = Console.ReadLine();
            switch (input)
            {
                case "/help":
                    foreach (var command in commands)
                    {
                        Console.WriteLine($"{command.Key} - {command.Value}");
                    }
                    break;

                case "/login":
                    Console.WriteLine("Enter your username");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter your password");
                    password = ReadPassword();
                    // Call the login method from the service layer
                    Task<HttpResponseMessage> loginResponse = CallPostEndpointAsync(endpoints["Auth"] + "/loginuser", new LoginDto { Password = password, Username = username});
                    while (!loginResponse.IsCompleted)
                    {
                        Console.Write(".");
                        Thread.Sleep(1000);
                    }
                    token = loginResponse.Result.ToString();
                    Console.WriteLine(token);
                    username = "";
                    password = "";
                    break;

                case "/logout":
                    Console.WriteLine("You have been logged out :) go outside, touch grass");
                    Thread.Sleep(1500);
                    run = false;
                    break;

                case "/adduser":
                    Console.WriteLine("Enter your username");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter your password");
                    password = ReadPassword();
                    Console.WriteLine("Enter your name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter your age");
                    int age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter your email");
                    string email = Console.ReadLine();
                    // Call the add user method from the service layer
                    Task<HttpResponseMessage> response = CallPostEndpointAsync(endpoints["User"] + $"/AddUser?username={username}&password={password}", new UserDTO { Id = 1, Name = name, Age = age, Email = email });
                    while (!response.IsCompleted)
                    {
                        Console.Write(".");
                        Thread.Sleep(1000);
                    }
                    if (response.Result.IsSuccessStatusCode)
                    {
                        Console.WriteLine("\nUser added successfully");
                    } else
                    {
                        Console.WriteLine("\nUser not added " + response.Result.ToString());
                    }
                    username = "";
                    password = "";
                    break;

                case "/exit":
                    run = false;
                    break;

                default:
                    if (input.Contains("/"))
                        Console.WriteLine("Invalid command");
                    break;
            }
        }
    }

    static Dictionary<string, string> ReadAppSettings(string filePath)
    {
        Dictionary<string, string> endpoints = new Dictionary<string, string>();

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
    static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(intercept: true);

            if (keyInfo.Key != ConsoleKey.Enter)
            {
                password += keyInfo.KeyChar;
                Console.Write("*");
            }
        } while (keyInfo.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }
    static async Task<HttpResponseMessage> CallPostEndpointAsync(string endpoint, Object data)
    {
        HttpClient client = new HttpClient();
        var response = await client.PostAsJsonAsync(endpoint, data);
        Console.WriteLine(response);
        return response; // Simulating a successful operation
    }
}