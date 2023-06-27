using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Migrations;

public class Program
{
    private const string ConnectionString =
        "Host=localhost;Port=5600;Database=test;Username=user;Password=password";
    
    public static void Main(string[] args)
    {
        var serviceProvider = CreateServices();
        
        using var scope = serviceProvider.CreateScope();

        UpdateDatabase(scope.ServiceProvider);
    }
    
    private static IServiceProvider CreateServices()
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(ConnectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }

    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}