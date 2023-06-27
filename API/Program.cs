using System.Data;
using System.Reflection;
using Data;
using Npgsql;
using Serilog;
using Serilog.Events;
using TestTask2.Options;
using Tomlyn.Extensions.Configuration;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

IConfigurationBuilder configuration = new ConfigurationBuilder();
configuration.AddTomlFile("config.toml");
var builtConfiguration = configuration.Build();
builder.Configuration.AddConfiguration(builtConfiguration);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DbOptions>(builtConfiguration.GetSection("Db"));

builder.Services.AddSingleton<IDbConnection>(new NpgsqlConnection(builtConfiguration["Db:ConnectionString"]));
builder.Services.Scan(scan => scan
    .FromAssemblies(Assembly.Load("Data"))
    .AddClasses(classes => classes.AssignableTo<ITransientProvider>())
    .AsImplementedInterfaces()
    .WithSingletonLifetime()
);

var app = builder.Build();

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyMethod().WithOrigins("http://localhost:5173");
});

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