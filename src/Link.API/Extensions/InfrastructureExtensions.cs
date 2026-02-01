using Link.Application.Interfaces;
using Link.Infrastructure.Persistence.Context;
using Link.Infrastructure.Persistence.Repositories;
using Link.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Link.API.Extensions;

public static class InfrastructureExtensions
{

    // This class can be used to add extension methods related to infrastructure services.

    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        // 1. Add EF Core Context
        service.AddDbContext<AppDbContext>(
            options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        // 2. Add Repositories (The Bridge)
        service.AddScoped<ILinkRepository, LinkRepository>();
        service.AddScoped<IAnalyticsRepository, AnalyticsRepository>();


        // 3. Add Infrastructure Service
        service.AddSingleton<IUrlShorteningService, UrlShorteningService>();

        return service;
    }
}