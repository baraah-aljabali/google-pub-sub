using Google.Cloud.PubSub.V1;
using Google.PubSub.Publisher;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Google.PubSub.Producer
{
    public class PublisherService : IPublisherService
    {
        private readonly GooglePublisherOptions _googlePublisherOptions;
        public PublisherService(IOptions<GooglePublisherOptions> publisherOptions)
        {
            _googlePublisherOptions = publisherOptions.Value;
        }

        public async Task<int> PublishMessagesAsync(PublisherRequest publisherRequest)
        {
            TopicName topicName = TopicName.FromProjectTopic(_googlePublisherOptions.ProjectId, _googlePublisherOptions.TopiId);
            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            int publishedMessageCount = 0;
            var publishTasks = publisherRequest.messageTexts.Select(async text =>
            {
                try
                {
                    string message = await publisher.PublishAsync(text);
                    Console.WriteLine($"Published message {message}");
                    Interlocked.Increment(ref publishedMessageCount);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"An error ocurred when publishing message {text}: {exception.Message}");
                }
            });
            await Task.WhenAll(publishTasks);
            return publishedMessageCount;
        }
    }
}
