using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Google.PubSub.Publisher
{
    public static class GooglePublisherRegistration
    {
        public static IServiceCollection AddGooglePubSubPublisherService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GooglePublisherOptions>(configuration.GetSection("GooglePublisher"));
            services.AddScoped<IPublisherService, Google.PubSub.Producer.PublisherService>();
            return services;
        }
    }
}
