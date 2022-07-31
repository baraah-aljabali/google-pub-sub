using System.Threading.Tasks;

namespace Google.PubSub.Subscriber
{
    public interface ISubscriberService
    {
        Task<int> PullMessagesAsync(SubscriberRequest subscriberRequest);
    }
}
