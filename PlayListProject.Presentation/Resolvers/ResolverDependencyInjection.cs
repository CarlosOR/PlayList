using PlayListProject.Presentation.Resolvers.Application;
using PlayListProject.Presentation.Resolvers.Presentation;

namespace PlayListProject.Presentation.Resolvers
{
    public static class ResolverDependencyInjection
    {
        public static void ResolveDependencyInjection(this IServiceCollection services)
        {
            services.ResolveApplication()
                .ResolvePresentation();
        }
    }
}
