using Serilog.Events;
using Serilog;
using System.Reflection;
using HealthMiddelWare;
using EasyNetQ;
using Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddSeq("http://localhost:5341");
});
builder.Services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));

CommentApplication.DependencyResolver.DependencyResolverService.RegisterServices(builder.Services);
CommentInfrastructure.DependencyResolver.DependencyResolverService.RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthReportingMiddleware("CommentService");

app.MapControllers();

app.Run();
