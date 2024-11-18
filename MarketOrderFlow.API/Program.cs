using Hangfire;
using MarketOrderFlow.API;
using MarketOrderFlow.API.Endpoints;
using MarketOrderFlow.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;

static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Logging.AddConsole();
        AddConfigurations(builder);
        AddServices(builder);
        WebApplication app = builder.Build();
        SetApp(app);
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            RecurringJob.AddOrUpdate<OrderService>(
                "GenerateDailyOrders",           
                service => service.GenerateDailyOrders(), // Çalýþtýrýlacak metot
                Cron.Daily);                      // Her gün çalýþacak þekilde zamanla
        });
        
        app.UseHangfireDashboard("/hangfire");
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
        builder.Services.AddAPIServices();

        var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
        builder.Services.AddAuthorization(ConfigureAuthorizationOptions);
        // Hangfire'ý Redis ile kaydet
        builder.Services.AddHangfireWithRedis(redisConnectionString);

    }
    private static void ConfigureDBContextOptions(IServiceProvider serviceProvider, DbContextOptionsBuilder options)
    {
        IConfiguration? configuration = serviceProvider.GetService<IConfiguration>() ?? throw new ApplicationException();
        options.UseSqlServer(configuration.GetConnectionString("MarketOrderFlow-DB"));
        options.LogTo(Console.WriteLine, LogLevel.Information);
        options.EnableSensitiveDataLogging();
    }

    private static void AddConfigurations(WebApplicationBuilder builder)
    {
        builder
            .Configuration
            .GetSection(nameof(MarketOrderFlowConfiguration.Authentication))
            .Bind(MarketOrderFlowConfiguration.Authentication);

        builder.Configuration
            .GetSection("Authentication:Schemes:Bearer")
            .Bind(MarketOrderFlowConfiguration.Authentication);
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
        app.MapGroup("/logistic").MapLogisticCenter().WithTags("Logistic Center Management API");
        app.MapGroup("/market").MapMarket().WithTags("Market Management API");
        app.MapGroup("/product").MapProduct().WithTags("Product Management API");
        app.MapGroup("/order").MapOrder().WithTags("Order Management API");
    }
    private static void ConfigureAuthorizationOptions(AuthorizationOptions o)
    {
        o.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
        o.AddPolicy("User", policy => policy.RequireRole("User"));
        o.AddPolicy("ManagerOrAdmin", policy => policy.RequireRole("Manager","Admin"));
        o.AddPolicy("AllRoles", policy => policy.RequireRole("User", "Manager", "Admin"));
    }

    private static void ConfigureJSONOptions(Microsoft.AspNetCore.Http.Json.JsonOptions o) =>
       o.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}
