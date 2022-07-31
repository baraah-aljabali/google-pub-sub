using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.PubSub.Subscriber
{
    public class GoogleSubscriberOptions
    {
        /// <summary>
        /// The GCP <c>Project</c> ID.
        /// </summary>
        public string ProjectId { get; set; }

        public string SubscriptionId { get; set; } = "my-sub";
    }
}
