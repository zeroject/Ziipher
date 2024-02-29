using Microsoft.Extensions.DependencyInjection;

namespace DirectMessageApplication;

public class DependencyResolverService
{
    public static void RegisterApplicationLayer(IServiceCollection services)
    {
        services.AddScoped<IDMService, DMService>();
    }
}
