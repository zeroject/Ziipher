using AspNetCoreRateLimit;
using AutoMapper;
using Domain;
using EasyNetQ;
using HealthMiddelWare;
using Messaging;
using Microsoft.Extensions.Configuration;
using PostApplication;
using PostApplication.DTO_s;
using PostInfrastructure;
using RateLimit;
using RateLimit.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

#region AutoMapper
var mapper = new MapperConfiguration(config =>
{
    config.CreateMap<PostPostDTO, Post>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion
builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));

builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddSeq("http://seq:5341");
});

#region Depedency injection
builder.Services.AddDbContext<RepositoryDBContext>();
builder.Services.AddScoped<IPostRepository, PostRepostiroy>(); ;
builder.Services.AddScoped<IPostService, PostService>();
#endregion


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
    app.UseHealthReportingMiddleware("PostService");
}

app.MapControllers();

app.Run();
