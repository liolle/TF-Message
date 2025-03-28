using DotNetEnv;
using ReplicaWorker;
using TFMessage.database;
using TFMessage.worker.service;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// Add Env &  Json configuration
Env.Load();
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<ReadDataContext>(new ReadDataContext(builder.Configuration["DB_READ_CONNECTION_STRING"]));
builder.Services.AddSingleton<IReplicaService,ReplicaService>();

var host = builder.Build();
host.Run();
