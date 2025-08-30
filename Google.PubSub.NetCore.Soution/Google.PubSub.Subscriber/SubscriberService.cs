using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace Google.PubSub.Subscriber
{
    public class SubscriberService : ISubscriberService
    {
        private readonly GoogleSubscriberOptions _googlePubSubOptions;
        private readonly SubscriberServiceApiClient _subscriberClient;

        public SubscriberService(IOptions<GoogleSubscriberOptions> pubsuboptions)
        {
            _googlePubSubOptions = pubsuboptions.Value;
            _subscriberClient = SubscriberServiceApiClient.Create();

        }
        public async Task<List<string>> PullMessagesAsync(SubscriberRequest subscriberRequest)
        {
            SubscriptionName subscriptionName = new(_googlePubSubOptions.ProjectId, subscriberRequest.sub_name);
            TopicName topic = new(_googlePubSubOptions.ProjectId, _googlePubSubOptions.TopiId);

            try
            {
                await _subscriberClient.CreateSubscriptionAsync(subscriptionName, topic, pushConfig: null, ackDeadlineSeconds: 60);
            }
            catch (RpcException ex) when (ex.Status.StatusCode == StatusCode.AlreadyExists)
            {
                Console.WriteLine("Subscription exists!");
            }
            Console.WriteLine("Subscription Created!");

            SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);
            // SubscriberClient runs your message handle function on multiple
            // threads to maximize throughput.
            int messageCount = 0;
            List<string> messages = [];

            Task startTask = subscriber.StartAsync((PubsubMessage message, CancellationToken cancel) =>
            {
                string text = System.Text.Encoding.UTF8.GetString(message.Data.ToArray());
                messages.Add(text);
                Console.WriteLine($"Message {message.MessageId}: {text}");
                Interlocked.Increment(ref messageCount);
                return Task.FromResult(subscriberRequest.acknowledge ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack);
            });
            // Run for 5 seconds.
            await Task.Delay(5000);
            await subscriber.StopAsync(CancellationToken.None);
            // Lets make sure that the start task finished successfully after the call to stop.
            await startTask;
            return messages;
        }
    }
}
