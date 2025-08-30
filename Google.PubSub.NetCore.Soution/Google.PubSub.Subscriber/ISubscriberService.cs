using System.Threading.Tasks;
using System.Collections.Generic;

namespace Google.PubSub.Subscriber
{
    public interface ISubscriberService
    {
        Task<List<string>> PullMessagesAsync(SubscriberRequest subscriberRequest);
    }
}
