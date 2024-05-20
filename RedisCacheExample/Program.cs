using Microsoft.EntityFrameworkCore;
using RedisCacheExample.Middleware;
using RedisCacheExample.Services;
using RedisMQ.Practice.Services;
using StackExchange.Redis;

namespace RedisCacheExample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configure PostgreSQL
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")));

        // Configure Redis
        builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
            configuration.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(configuration);
        });

        // Register RedisService
        builder.Services.AddSingleton<RedisService>();

        // Register ProductService
        builder.Services.AddTransient<ProductService>();

        // Register RateLimiterService
        builder.Services.AddSingleton<RateLimiterService>();

        // Configure Redis distributed cache
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
            options.InstanceName = "RedisCacheExample:";
        });

        // Configure session management
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Register IHttpContextAccessor
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Register SessionService
        builder.Services.AddTransient<SessionService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSession(); // Enable session middleware

        // Use Rate Limiting Middleware
        app.UseMiddleware<RateLimitingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
