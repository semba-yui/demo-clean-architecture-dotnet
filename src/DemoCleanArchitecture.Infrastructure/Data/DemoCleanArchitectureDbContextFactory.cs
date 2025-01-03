using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Data;

/// <summary>
///     デザイン時に DbContext を生成するためのファクトリ
/// </summary>
internal sealed class
    DemoCleanArchitectureDbContextFactory : IDesignTimeDbContextFactory<DemoCleanArchitectureDbContext>
{
    /// <summary>
    ///     デザイン時に DbContext を生成する
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public DemoCleanArchitectureDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        Console.WriteLine(@"Environment: " + environment);

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile($"appsettings.{environment}.json", true)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var builder = new DbContextOptionsBuilder<DemoCleanArchitectureDbContext>();
        builder.UseSqlServer(connectionString);

        var context = new DemoCleanArchitectureDbContext(builder.Options);

        return context;
    }
}
