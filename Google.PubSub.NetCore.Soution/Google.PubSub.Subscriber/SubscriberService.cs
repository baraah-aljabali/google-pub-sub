using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Google.PubSub.Subscriber
{
    public class SubscriberService : ISubscriberService
    {
        private readonly GoogleSubscriberOptions _googlePubSubOptions;
        public SubscriberService(IOptions<GoogleSubscriberOptions> pubsuboptions)
        {
            _googlePubSubOptions = pubsuboptions.Value;
        }
        public async Task<int> PullMessagesAsync(SubscriberRequest subscriberRequest)
        {
            SubscriptionName subscriptionName = SubscriptionName.FromProjectSubscription(_googlePubSubOptions.ProjectId, _googlePubSubOptions.SubscriptionId);
            SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);
            // SubscriberClient runs your message handle function on multiple
            // threads to maximize throughput.
            int messageCount = 0;
            Task startTask = subscriber.StartAsync((PubsubMessage message, CancellationToken cancel) =>
            {
                string text = System.Text.Encoding.UTF8.GetString(message.Data.ToArray());
                Console.WriteLine($"Message {message.MessageId}: {text}");
                Interlocked.Increment(ref messageCount);
                return Task.FromResult(subscriberRequest.acknowledge ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack);
            });
            // Run for 5 seconds.
            await Task.Delay(5000);
            await subscriber.StopAsync(CancellationToken.None);
            // Lets make sure that the start task finished successfully after the call to stop.
            await startTask;
            return messageCount;
        }
    }
}
