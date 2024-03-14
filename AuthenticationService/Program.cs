using AuthenticationService;
using AuthenticationService.BE;
using AuthenticationService.Dto;
using AuthenticationService.helpers;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using AuthenticationService.Repos;
using AutoMapper;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//add mapper objects
var mapperConfig = new MapperConfiguration(conf =>
{
    //from BE to DTO
    conf.CreateMap<Login, LoginDto>();

    //from DTO to BE ignore the Id
    conf.CreateMap<LoginDto, Login>();
});

var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AuthContext>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IAuthValidationService, AuthValidationService>();

//add appsettings secret key to the helper AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//add identity server
builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetApiResources())
    .AddInMemoryClients(Config.GetClients())
    .AddResourceOwnerValidator<tokenValidator>(); // Add custom token validator


builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddLogging(logBuilder =>
{
    logBuilder.AddSeq("http://seq:5341");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseIdentityServer();

app.Run();
