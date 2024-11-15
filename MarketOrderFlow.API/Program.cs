
using MarketOrderFlow.API;
using MarketOrderFlow.API.Endpoints;
using MarketOrderFlow.API.Utility;
using MarketOrderFlow.Infrastructure;
using MarketOrderFlow.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Logging.AddConsole();
        AddServices(builder);
        WebApplication app = builder.Build();
        SetApp(app);
        app.Run();
    }
    private static void AddServices(WebApplicationBuilder builder)
    {

        builder.Services.ConfigureHttpJsonOptions(ConfigureJSONOptions);
        builder.Services.AddDbContext<ApplicationDbContext>(ConfigureDBContextOptions);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services
           .AddAuthentication(ConfigureAuthenticationOptions)
           .AddJwtBearer(ConfigureJwtBearerOptions);
        builder.Services
      .AddIdentity<UserModel, IdentityRole>(ConfigureIdentityOptions)
      .AddErrorDescriber<MarketFlowIdentityErrorDescriber>()
      .AddEntityFrameworkStores<ApplicationDbContext>()
      .AddDefaultTokenProviders();

    }
    private static void ConfigureDBContextOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder options)
    {
        IConfiguration? configuration = serviceProvider.GetService<IConfiguration>() ?? throw new ApplicationException();
        options.UseSqlServer(configuration.GetConnectionString("MarketOrderFlow-DB"));
        options.LogTo(Console.WriteLine, LogLevel.Information); //TODO release modunda loglamayý yapmamalý
        options.EnableSensitiveDataLogging(); //TODO release modunda olmamalý
    }
    private static void ConfigureJwtBearerOptions(JwtBearerOptions options)
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = MarketOrderFlowConfiguration.Authentication.ValidIssuer,
            ValidAudiences = MarketOrderFlowConfiguration.Authentication.ValidAudiences,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MarketOrderFlowConfiguration.Authentication.SecurityKey))
        };
    }
    private static void ConfigureIdentityOptions(IdentityOptions options)
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
    }

    private static void ConfigureAuthenticationOptions(AuthenticationOptions options)
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    private static void SetApp(WebApplication app)
    {
        app.UseAuthentication();
        //app.UseCors();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        MapGroups(app);
    }

    private static void MapGroups(WebApplication app)
    {
        app.MapGroup("/auth").MapAuthApi().WithTags("Authentication & Authorization Management API");
    }
    private static void ConfigureJSONOptions(Microsoft.AspNetCore.Http.Json.JsonOptions o) =>
       o.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}
