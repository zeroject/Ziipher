using HealthMiddelWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddSeq("http://seq:5341");
});

DirectMessageApplication.DependencyResolverService.RegisterApplicationLayer(builder.Services);
DirectMessageInfrastructure.DependencyResolverService.RegisterInfrastructureLayer(builder.Services);

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

app.UseHealthReportingMiddleware("DMService");

app.MapControllers();

app.Run();

