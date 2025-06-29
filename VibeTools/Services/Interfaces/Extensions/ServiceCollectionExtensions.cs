using Microsoft.EntityFrameworkCore;
using VibeTools.Data;
using VibeTools.Repositories;
using VibeTools.Repositories.Interfaces;
using VibeTools.Services;
using VibeTools.Services.Interfaces;

namespace VibeTools.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<VibeToolsContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IToolRepository, ToolRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        // Services
        services.AddScoped<IToolService, ToolService>();

        // CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
            
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        return services;
    }
}