using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var authenticationProviderKey = "Bearer";

builder.Services.AddAuthentication().AddJwtBearer(authenticationProviderKey, options =>
{
    options.Authority = "https://authservice";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        
    };
});

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();
