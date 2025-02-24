using Microsoft.Extensions.DependencyInjection;
using PreziViewer.Services.Interface;

namespace PreziViewer.Services
{
    public static class RegistrationsExtension
    {
        public static void WithPresentationCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IPresentationFetcher, PresentationFetcher>();
            services.AddSingleton<IPresentationOnlineFetcher, PresentationOnlineFetcher>();
            services.AddSingleton<IPresentationLocalFetcher, PresentationLocalFetcher>();
            services.AddSingleton<IConfigurationService, ConfigurationService>();
            services.AddTransient<HttpClient>();
        }
    }
}