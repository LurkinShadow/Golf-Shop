using Golf.Shop.Application.Abstractions;
using Golf.Shop.Infrastructure.Configuration;
using Golf.Shop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Golf.Shop.Infrastructure;

public static class Bindings
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IStorage, Storage>();
        services.AddDbContext<RepairOrderContext>(dbContextOptions =>
            dbContextOptions.UseNpgsql(configuration.GetConnectionString("RepairOrderDBConnectionString")));

        return services;
    }
}

