using DotNetEnv;
using TFMessage.database;
using TFMessage.read.service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add Env &  Json configuration
Env.Load();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ReadDataContext>(new ReadDataContext(builder.Configuration["DB_READ_CONNECTION_STRING"]));
builder.Services.AddScoped<IReadService,ReadService>();

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
