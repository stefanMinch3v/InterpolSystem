using Microsoft.Extensions.DependencyInjection;

namespace InterpolSystem.Languages
{
    public static class ContainerExtensions
    {
        public static IServiceCollection AddLanguagesDependencies(this IServiceCollection services)
        {
            services.AddTransient<ILanguageService, LanguageService>();

            return services;
        }
    }
}
