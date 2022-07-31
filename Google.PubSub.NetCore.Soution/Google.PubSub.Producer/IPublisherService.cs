using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.PubSub.Publisher
{
    public interface IPublisherService
    {
        public Task<int> PublishMessagesAsync(string projectId, string topicId, IEnumerable<string> messageTexts);
    }
}
