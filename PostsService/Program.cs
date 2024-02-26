using AutoMapper;
using Domain;
using PostApplication;
using PostApplication.DTO_s;
using PostInfrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper
var mapper = new MapperConfiguration(config =>
{
    config.CreateMap<PostTimelineDTO, Timeline>();
    config.CreateMap<PutTimelineDTO, Timeline>();
    config.CreateMap<DeleteTimelineDTO, Timeline>();
}).CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region Depedency injection
builder.Services.AddDbContext<RepositoryDBContext>();
builder.Services.AddScoped<RepositoryDBContext>();
builder.Services.AddScoped<IPostRepository, PostRepostiroy>(); ;
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ITimelineRepository, TimelineRepository>();
builder.Services.AddScoped<ITimelineService, TimelineService>();
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

app.MapControllers();

app.Run();
