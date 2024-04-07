using AspNetCoreRateLimit;
using EasyNetQ;
using HealthMiddelWare;
using Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure rate limiting services
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>((options) =>
{
    options.GeneralRules = new System.Collections.Generic.List<RateLimitRule>()
    {
        new RateLimitRule
        {
            Endpoint = "*",
            Limit = 100, // Adjust the limit as needed
            Period = "1m" // Adjust the period as needed (e.g., 1 minute)
        }
    };
});
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
// Register the default processing strategy
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
// Register the rate limit configuration
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddSeq("http://seq:5341");
});

LikeApplication.DependencyResolverService.RegisterApplicationLayer(builder.Services);
LikeInfrastructure.DependencyResolverService.RegisterInfrastructureLayer(builder.Services);
builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseIpRateLimiting();

if (app.Environment.IsProduction())
{
    app.UseHealthReportingMiddleware("LikeService");
}

app.MapControllers();

app.Run();
