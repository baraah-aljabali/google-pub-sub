using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.PubSub.Subscriber
{
    public static class GooglePubSubSubscriberServiceRegistration
    {
        public IServiceCollection AddGooglePubSubSubscriberService(this IServiceCollection services)
        {
            services.Configure<>
            return services;
        }
    }
}
