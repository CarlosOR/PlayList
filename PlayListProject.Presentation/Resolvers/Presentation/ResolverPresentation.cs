using Microsoft.AspNetCore.Mvc;
using PlayListProject.Presentation.Middleware;

namespace PlayListProject.Presentation.Resolvers.Presentation
{
    public static class ResolverPresentation
    {
        public static IServiceCollection ResolvePresentation(this IServiceCollection services)
        {
            services.AddScoped<GlobalExceptionHandler>();
            services.AvoidAutomaticValidation();
            return services;
        }

        public static IServiceCollection AvoidAutomaticValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            return services;
        }
    }
}
