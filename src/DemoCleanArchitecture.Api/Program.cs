using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using DemoCompany.DemoCleanArchitecture.Api.Filters;
using DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1;
using DemoCompany.DemoCleanArchitecture.Api.Middlewares;
using DemoCompany.DemoCleanArchitecture.Api.Utils;
using DemoCompany.DemoCleanArchitecture.Application.Constants;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Application.Services.Auths;
using DemoCompany.DemoCleanArchitecture.Application.Services.Permissions;
using DemoCompany.DemoCleanArchitecture.Application.Services.Roles;
using DemoCompany.DemoCleanArchitecture.Application.Services.Users;
using DemoCompany.DemoCleanArchitecture.Domain.Constants;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Domain.Settings;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using DemoCompany.DemoCleanArchitecture.Infrastructure.HealthChecks;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Securities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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

// Add Repositories
builder.Services.AddScoped<IAuthCodeRepository, AuthCodeRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
// 日付はシステム全体で共通のため Singleton で登録
builder.Services.AddSingleton<IDateTimeRepository, DateTimeRepository>();

// Add Providers
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>();

// Add Services
builder.Services.AddScoped<IssueTwoFactorCodeService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<LogoutService>();
builder.Services.AddScoped<RegisterUserService>();
builder.Services.AddScoped<VerifyTwoFactorCodeService>();
builder.Services.AddScoped<AddPermissionService>();
builder.Services.AddScoped<DeletePermissionService>();
builder.Services.AddScoped<GetPermissionService>();
builder.Services.AddScoped<GetPermissionsForUserService>();
builder.Services.AddScoped<UpdatePermissionService>();
builder.Services.AddScoped<AddRoleService>();
builder.Services.AddScoped<DeleteRoleService>();
builder.Services.AddScoped<GetRoleService>();
builder.Services.AddScoped<UpdateRoleService>();
builder.Services.AddScoped<DeleteUserService>();
builder.Services.AddScoped<GetUserService>();

builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

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

// Add Controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
    options.Filters.Add<CustomValidateActionFilterAttribute>();

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

// 認証の設定
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        // JWT 検証用のパラメータを設定
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["DemoCleanArchitectureSettings:JwtIssuer"],
            ValidAudience = builder.Configuration["DemoCleanArchitectureSettings:JwtAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["DemoCleanArchitectureSettings:JwtSecret"]!)
            ),
            ClockSkew = TimeSpan.Zero
        };

        // 認証失敗時のレスポンスをカスタム
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // デフォルトの動作をスキップ
                context.HandleResponse();

                // HTTPステータスコードを 401
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                // JSON ボディを返す
                var result = new ErrorResponse { Code = ErrorCodes.Unauthorized, Message = "Unauthorized Error" };
                return context.Response.WriteAsJsonAsync(result);
            }
        };
    });

// 認可の設定
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(DemoCleanArchitectureConstants.LimitedAccessRole, policy =>
    {
        policy.RequireAssertion(context =>
        {
            // JWT のクレームから2段階認証が完了しているかを取得
            var claim = context.User.FindFirst(DemoCleanArchitectureConstants.TwoFaVerifiedClaim);
            return (claim?.Value == true.ToString());
        });
    })
    .AddPolicy(PermissionNames.UserCreate,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.UserCreate)); })
    .AddPolicy(PermissionNames.UserRead,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.UserRead)); })
    .AddPolicy(PermissionNames.UserUpdate,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.UserUpdate)); })
    .AddPolicy(PermissionNames.UserDelete,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.UserDelete)); })
    .AddPolicy(PermissionNames.RoleCreate,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.RoleCreate)); })
    .AddPolicy(PermissionNames.RoleRead,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.RoleRead)); })
    .AddPolicy(PermissionNames.RoleUpdate,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.RoleUpdate)); })
    .AddPolicy(PermissionNames.RoleDelete,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.RoleDelete)); })
    .AddPolicy(PermissionNames.PermissionCreate,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.PermissionCreate)); })
    .AddPolicy(PermissionNames.PermissionRead,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.PermissionRead)); })
    .AddPolicy(PermissionNames.PermissionUpdate,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.PermissionUpdate)); })
    .AddPolicy(PermissionNames.PermissionDelete,
        policy => { policy.Requirements.Add(new PermissionRequirement(PermissionNames.PermissionDelete)); });

builder.Services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
builder.Services.AddScoped<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

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
