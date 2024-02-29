using DirectMessageApplication;
using Microsoft.Extensions.DependencyInjection;

namespace DirectMessageInfrastructure;

public class DependencyResolverService
{
public static void RegisterInfrastructureLayer(IServiceCollection services)
    {
        services.AddScoped<IDMRepository, DMRepository>();
    }
}
