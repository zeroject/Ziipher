using AuthenticationService.Dto;
using Domain;
using Microsoft.AspNetCore.Http;
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

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract service name from request headers or query parameters
            string serviceName = context.Request.Headers["ServiceName"];

            if (string.IsNullOrEmpty(serviceName))
            {
                context.Response.StatusCode = 400; // Bad request
                await context.Response.WriteAsync("ServiceName header is missing.");
                return;
            }

            // Dummy logic to determine health status
            Health serviceHealth = DetermineHealth(serviceName);

            // Report health status to HealthService
            await PostHealthAsync(serviceHealth);

            // Call the next middleware in the pipeline
            await _next(context);
        }

        private Health DetermineHealth(string serviceName)
        {
            Health health = new Health
            {
                ServiceName = serviceName,
                Status = HealthStatus.Healthy,
                Timestamp = DateTime.UtcNow
            };

            return health;
        }

        private async Task PostHealthAsync(Health serviceHealth)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://healthservice:8080");
                var response = await client.PostAsJsonAsync("/auth/registerNewLogin", serviceHealth);
            }
        }
    }
}
