using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Google.PubSub.Subscriber
{
    public static class GoogleSubscribeRegistration
    {
        public static IServiceCollection AddGooglePubSubSubscriberService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GoogleSubscriberOptions>(configuration.GetSection("GoogleSubscriber"));
            return services;
        }
    }
}
