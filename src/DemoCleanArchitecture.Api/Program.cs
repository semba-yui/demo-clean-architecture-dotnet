using System.Text.Json;
using System.Text.Json.Serialization;
using DemoCompany.DemoCleanArchitecture.Api.Filters;
using DemoCompany.DemoCleanArchitecture.Api.Middlewares;
using DemoCompany.DemoCleanArchitecture.Api.Utils;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Application.Services;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Domain.Settings;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using DemoCompany.DemoCleanArchitecture.Infrastructure.HealthChecks;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Load configurations
builder
    .Configuration
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

Log.Information("Starting the application...");

// Add services to the container.
builder.Services
    .AddLocalization(options =>
    {
        options.ResourcesPath = "Resources";
    });

// Add settings
builder.Services.AddOptions<DemoCleanArchitectureSettings>()
    .Bind(builder.Configuration.GetSection("DemoCleanArchitectureSettings"))
    .ValidateDataAnnotations();

// Add DateTimeRepository Before DbContext
builder.Services.AddSingleton<IDateTimeRepository, DateTimeRepository>();

// Add DbContext
builder.Services.AddDbContext<DemoCleanArchitectureDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        // 開発環境の場合は、各種ログをコンソールに出力する
        options.LogTo(Console.WriteLine);
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.CommandTimeout(30);
    });
});

// Add Repositories
builder.Services.AddScoped<IAuthCodeRepository, AuthCodeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();

// Add Services
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<LogoutService>();
builder.Services.AddScoped<RegisterUserService>();

builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

// Add Controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
    options.Filters.Add<CustomValidationFilter>();

    // URLパスをケバブケースに変換する
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseUpper));
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    // デフォルトのバリデーションエラーハンドリングを無効化
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddApiVersioning();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add HealthChecks
builder.Services.AddHealthChecks()
    .AddCheck<StartupHealthCheck>("startup")
    .AddCheck<DbConnectivityHealthCheck>("db")
    .AddCheck<LiveHealthCheck>("live");

// StartupHealthCheck は 状態を持つため DI コンテナに Singleton で登録
builder.Services.AddSingleton<StartupHealthCheck>();

var app = builder.Build();

app.UseSerilogRequestLogging();

var supportedCultures = new[] { "en", "ja-JP" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.MapHealthChecks("/health/startup",
        new HealthCheckOptions
        {
            Predicate = check => check.Name == "startup", ResponseWriter = HealthCheckUtil.WriteResponse
        })
    .AllowAnonymous();

app.MapHealthChecks("/health/readiness",
        new HealthCheckOptions
        {
            Predicate = check => check.Name == "db", ResponseWriter = HealthCheckUtil.WriteResponse
        })
    .AllowAnonymous();

app.MapHealthChecks("/health/liveness",
        new HealthCheckOptions
        {
            Predicate = check => check.Name == "live", ResponseWriter = HealthCheckUtil.WriteResponse
        })
    .AllowAnonymous();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// 起動完了
var startupHealthCheck = app.Services.GetRequiredService<StartupHealthCheck>();
startupHealthCheck.MarkStartupComplete();

await app.RunAsync();

/// <summary>
///     For Integration Test.
/// </summary>
public abstract partial class Program;
