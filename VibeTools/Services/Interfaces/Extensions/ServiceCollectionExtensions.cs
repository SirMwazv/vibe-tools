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
            options.UseInMemoryDatabase("VibeToolsDb"));

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
        });

        return services;
    }
}