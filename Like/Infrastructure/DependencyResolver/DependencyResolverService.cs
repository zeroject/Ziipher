using LikeApplication;
using Microsoft.Extensions.DependencyInjection;

namespace LikeInfrastructure;

public class DependencyResolverService
{
    public static void RegisterInfrastructureLayer(IServiceCollection services)
    {
        services.AddScoped<ILikeRepository, LikeRepository>();
    }
}
