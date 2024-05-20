using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservarTurnos.Infraestructure.Interfaces;
using ReservarTurnos.Infraestructure.Persistence;
using ReservarTurnos.Infraestructure.Repositories;

namespace ReservarTurnos.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<ITradeRepository, TradeRepository>()
                .AddScoped<IShiftRepository, ShiftRepository>()
                .AddScoped<IServiceRepository, ServiceRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
