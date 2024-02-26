using AuthenticationService.Dto;
using ConsoleAppUI;
using ConsoleAppUI.Commands;
using ConsoleAppUI.Commands.ImplementedCommands;
using ConsoleAppUI.Helper;
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
    public static commandsEnums.Commands type = commandsEnums.Commands.mainMenu;
    #endregion

    static void Main(string[] args)
    {
        string jsonFilePath = "appsettings.json";
        AllCommands.populateCommands();
        EndPoints.PopulateEndPoints(jsonFilePath);

        while (run)
        {
            //check if user is writing in the console
            if (Console.KeyAvailable)
            {
                //check if first character is a '/'
                if (Console.ReadKey().KeyChar == '/')
                {
                    string command = Console.ReadLine().TrimStart('/');
                    if (AllCommands.commands.ContainsKey(command))
                    {
                        ICommands ToBeExecuted = AllCommands.commands[command];
                        if (ToBeExecuted.type == type || ToBeExecuted.type == commandsEnums.Commands.global)
                        {
                            ToBeExecuted.Execute();
                        }
                        else
                        {
                            Console.WriteLine("Command not available in this context");
                        
                        }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command not found");
                    }
                }
            }
        }
    } 