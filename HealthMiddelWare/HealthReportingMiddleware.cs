using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Net.Http.Json;

namespace HealthMiddelWare
{
    public class HealthReportingMiddleware
    {
        private readonly RequestDelegate _next;

        public HealthReportingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, string serviceName)
        {
            // Dummy logic to determine health status
            Health serviceHealth = await DetermineHealthAsync(serviceName);

            // Report health status to HealthService
            await PostHealthAsync(serviceHealth);

            // Call the next middleware in the pipeline
            await _next(context);
        }

        private async Task<Health> DetermineHealthAsync(string serviceName)
        {
            int score = 0;

            ResourceUsage resourceUsage = new ResourceUsage();
            CheckConnections checkConnections = new CheckConnections();

            if (await checkConnections.CheckServiceResponseTime())
            {
                score++;
            }

            double ramLeft = resourceUsage.GetRemainingRAMUsage();
            double diskSpaceLeft = resourceUsage.GetRemainingDiskSpace();

            if (ramLeft > 20)
            {
                score++;
            }

            if (diskSpaceLeft > 20)
            {
                score++;
            }

            Health health = new Health
            {
                ServiceName = serviceName,
                Status = HealthStatus.Healthy,
                Timestamp = DateTime.UtcNow
            };

            switch (score)
            {
                case 0:
                    health.Status = HealthStatus.Dying;
                    break;
                case 1:
                    health.Status = HealthStatus.Dying;
                    break;
                case 2:
                    health.Status = HealthStatus.Sick;
                    break;
                case 3:
                    health.Status = HealthStatus.Healthy;
                    break;
                default:
                    health.Status = HealthStatus.Unknown;
                    break;
            }

            return health;
        }

        private async Task PostHealthAsync(Health serviceHealth)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://healthservice:8080");
                var response = await client.PostAsJsonAsync("/health", serviceHealth);
            }
        }
    }

    public static class HealthReportingMiddlewareExtensions
    {
        public static IApplicationBuilder UseHealthReportingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HealthReportingMiddleware>();
        }
    }

    public class ResourceUsage
    {
        public double GetRemainingRAMUsage()
        {
            var process = Process.GetCurrentProcess();
            var ramUsage = process.WorkingSet64; // Memory usage in bytes
            var ramUsageMB = ramUsage / (1024 * 1024); // Convert to MB
            var totalRAM = Environment.WorkingSet; // Total available memory of the system

            var remainingRAM = totalRAM - ramUsageMB;
            return remainingRAM;
        }

        public double GetRemainingDiskSpace(string driveName = "/")
        {
            DriveInfo drive = new DriveInfo(driveName);
            double remainingSpace = drive.AvailableFreeSpace / (1024 * 1024); // Convert to MB
            return remainingSpace;
        }
    }

    public class CheckConnections
    {
        public async Task<bool> CheckServiceResponseTime()
        {
            try
            {
                var client = new HttpClient();
                var stopwatch = Stopwatch.StartNew();
                var response = await client.GetAsync("http://healthservice:8080/ping");
                stopwatch.Stop();
                TimeSpan responseTime = stopwatch.Elapsed;

                if (responseTime.TotalMilliseconds > 1000)
                {
                    return false;
                }

                return response.IsSuccessStatusCode; // Check if response is successful
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return false; // Service not responding
            }
        }
    }
}
