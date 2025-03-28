using DotNetEnv;
using TFMessage.database;
using TFMessage.write.services;
var builder = WebApplication.CreateBuilder(args);

// Add Env &  Json configuration
Env.Load();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FileManagerService>();
builder.Services.AddSingleton<WriteDataContext>(new WriteDataContext(builder.Configuration["DB_WRITE_CONNECTION_STRING"]));
builder.Services.AddScoped<IWriteService,WriteService>();
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
