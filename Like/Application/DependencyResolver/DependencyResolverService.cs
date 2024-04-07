using Microsoft.Extensions.DependencyInjection;

namespace LikeApplication;

public class DependencyResolverService
{
    public static void RegisterApplicationLayer(IServiceCollection services)
    {
        services.AddScoped<ILikeService, LikeService>();
    }
}
