using PlayListProject.Application.Services;
using PlayListProject.Application.IServices;

namespace PlayListProject.Presentation.Resolvers.Application
{
    public static class ResolverApplication
    {
        public static IServiceCollection ResolveApplication(this IServiceCollection services)
        {
            services.AddScoped<IPlayListApplicationService, PlayListApplicationService>();
            services.AddScoped<ISongApplicationService, SongApplicationService>();
            services.AddScoped<IAuthorApplicationService, AuthorApplicationService>();
            services.AddScoped<IPlayListSongApplicationService, PlayListSongApplicationService>();
            return services;
        }
    }
}
