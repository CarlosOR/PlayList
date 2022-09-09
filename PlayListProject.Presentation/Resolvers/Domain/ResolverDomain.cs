using Microsoft.EntityFrameworkCore;
using PlayListProject.Domain.Context;

namespace PlayListProject.Presentation.Resolvers.Domain
{
    public static class ResolverDomain
    {

        public static void ResolveContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<PlayListProjectDbContext>(options =>
                options.UseSqlite(connection)
            );
        }
    }
}
