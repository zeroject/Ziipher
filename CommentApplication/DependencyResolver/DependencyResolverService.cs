using Microsoft.Extensions.DependencyInjection;

namespace CommentApplication.DependencyResolver
{
    public class DependencyResolverService
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ICommentCrud, CommentCrud>();
        }
    }
}
