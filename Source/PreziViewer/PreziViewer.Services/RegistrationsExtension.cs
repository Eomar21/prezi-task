using Microsoft.Extensions.DependencyInjection;
using PreziViewer.Services.Interface;

namespace PreziViewer.Services
{
    public static class RegistrationsExtension
    {
        public static void WithPresentationCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IPresentationFetcher, PresentationFetcher>();
            services.AddTransient<IPresentationOnlineFetcher, PresentationOnlineFetcher>();
            services.AddTransient<IPresentationLocalFetcher, PresentationLocalFetcher>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<HttpClient>();
        }
    }
}