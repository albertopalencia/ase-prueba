using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReservarTurnos.Infraestructure;
using System.Reflection;

namespace ReservarTurnos.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(conf => conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddInfraestructureServices(configuration);

        return services;
    }
}
